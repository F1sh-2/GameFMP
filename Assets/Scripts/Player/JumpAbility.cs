using UnityEngine;
using UnityEngine.InputSystem;

public class JumpAbility : BaseAbility
{
    public InputActionReference jumpActionRef;

    [SerializeField] private float jumpForce;
    [SerializeField] private float airSpeed;
    [SerializeField] private float mininumAirTime;
    private float startmininumAirTime;


    protected override void Initialization()
    {
        base.Initialization();
        startmininumAirTime = mininumAirTime;
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
    }
    public override void ProcessFixedAbility()
    {
        if (!linkedPhysics.grounded)
        {
            linkedPhysics.rb.linearVelocity = new Vector2(airSpeed * linkedInput.horizontalInput, linkedPhysics.rb.linearVelocityY);
        }
    }
    private void TryToJump(InputAction.CallbackContext value)
    {
        if(isPermitted==false) 
            return;
        Debug.Log(linkedPhysics.grounded);
        if (linkedPhysics.grounded)
        {
            linkedStateMachine.ChangeState(PlayerStates.State.Jump);
            linkedPhysics.rb.linearVelocity = new Vector2(airSpeed * linkedInput.horizontalInput, jumpForce);
            mininumAirTime = startmininumAirTime;
        }
    }

    private void StopJump(InputAction.CallbackContext value)
    {
        Debug.Log("STOPJUMP");
    }

}
