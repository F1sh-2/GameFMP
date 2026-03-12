using UnityEngine;
using UnityEngine.InputSystem;

public class JumpAbility : BaseAbility
{
    public InputActionReference jumpActionRef;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float airSpeed;
    [SerializeField] private float mininumAirTime;
    private float startmininumAirTime;

    private float jumpForce = 42;
    private string jumpAnimParameterName = "Jump";
    private string ySpeedAnimParameterName = "ySpeed";
    private int jumpParameterID;
    private int ySpeedParameterID;

    private void CalculateJumpForce()
    {
        float gravity = Physics2D.gravity.y * linkedPhysics.gravityValue;
        jumpForce = Mathf.Sqrt(-2.0f * gravity * jumpHeight);
    }

    protected override void Initialization()
    {
        base.Initialization();
        startmininumAirTime = mininumAirTime;
        jumpParameterID = Animator.StringToHash(jumpAnimParameterName);
        ySpeedParameterID = Animator.StringToHash(ySpeedAnimParameterName);
        CalculateJumpForce();
    }
    private void OnEnable()
    {
        jumpActionRef.action.performed += TryToJump;
    }

    private void OnDisable()
    {
        jumpActionRef.action.performed -= TryToJump;
        jumpActionRef.action.canceled -= StopJump;

    }
    public override void ProcessAbility()
    {
        mininumAirTime -= Time.deltaTime;
        if(linkedPhysics.grounded && mininumAirTime< 0)
        {
            linkedStateMachine.ChangeState(PlayerStates.State.Idle);
        }
        if(!linkedPhysics.grounded && linkedPhysics.wallDetected)
        {
            if(linkedPhysics.rb.linearVelocityY < 0)
            {
                linkedStateMachine.ChangeState(PlayerStates.State.WallSlide);
            }
        }
    }
    public override void ProcessFixedAbility()
    {
        if (!linkedPhysics.grounded)
        {
            linkedPhysics.rb.linearVelocity = new Vector2(airSpeed * linkedInput.horizontalInput, linkedPhysics.rb.linearVelocityY);
        }
    }

    private void Jump()
    {
        // This allows us to change the jump height or gravity scale in
        // the editor while the game is running
#if UNITY_EDITOR
        CalculateJumpForce();
#endif
        linkedPhysics.rb.linearVelocity = new Vector2(airSpeed * linkedInput.horizontalInput, jumpForce);
        mininumAirTime = startmininumAirTime;
    }

    private void TryToJump(InputAction.CallbackContext value)
    {
        if (isPermitted == false)
        {
            return;
        }

        if (linkedStateMachine.currentState == PlayerStates.State.Ladders || linkedPhysics.grounded)
        {
            linkedStateMachine.ChangeState(PlayerStates.State.Jump);
            Jump();
        }
    }

    private void StopJump(InputAction.CallbackContext value)
    {
        Debug.Log("STOPJUMP");
    }
    public override void UpdateAnimator()
    {
        linkedAnimator.SetBool(jumpParameterID, linkedStateMachine.currentState == PlayerStates.State.Jump || linkedStateMachine.currentState==PlayerStates.State.WallJump);
        linkedAnimator.SetFloat(ySpeedParameterID, linkedPhysics.rb.linearVelocityY);
    }

}
