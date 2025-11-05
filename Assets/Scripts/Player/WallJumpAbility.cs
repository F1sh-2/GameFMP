using UnityEngine;
using UnityEngine.InputSystem;

public class WallJumpAbility : BaseAbility
{
    public InputActionReference wallJumpActionRef;
    [SerializeField] private Vector2 wallJumpForce;
    [SerializeField] private float wallJumpMaxTime;
    private float wallJumpMininumTime;
    private float wallJumpTimer;

    private void OnEnable()
    {
        wallJumpActionRef.action.performed += TryToWallJump;
    }
    private void OnDisable()
    {
        wallJumpActionRef.action.performed -= TryToWallJump;
    }

    protected override void Initialization()
    {
        base.Initialization();
        wallJumpTimer = wallJumpMaxTime;

    }
    private void TryToWallJump(InputAction.CallbackContext value)
    {

        if (!isPermitted)
        {
            return;
        }

        if (EvaluateWallJumpConditions())
        {
            linkedStateMachine.ChangeState(PlayerStates.State.WallJump);
            wallJumpTimer = wallJumpMaxTime;
            wallJumpMininumTime = 0.15f;
            player.ForceFlip();
            if (player.facingRight)
            {
                linkedPhysics.rb.linearVelocity = new Vector2(wallJumpForce.x, wallJumpForce.y);
            }
            else
            {
                linkedPhysics.rb.linearVelocity = new Vector2(-wallJumpForce.x, wallJumpForce.y);
            }
                

        }
    }

    public override void ProcessAbility()
    {
        wallJumpTimer -= Time.deltaTime;
        wallJumpMininumTime -= Time.deltaTime;
        if (wallJumpMininumTime <=0 && linkedPhysics.wallDetected)
        {
            linkedStateMachine.ChangeState(PlayerStates.State.Jump);
            wallJumpTimer = -1;
            return;
        }
        if(wallJumpTimer <= 0)
        {
            if (linkedPhysics.grounded)
            {
                linkedStateMachine.ChangeState(PlayerStates.State.Idle);
            }
            else
            {
                linkedStateMachine.ChangeState(PlayerStates.State.Jump);
            }
        }
    }
    private bool EvaluateWallJumpConditions()
    {
        if(linkedPhysics.grounded || !linkedPhysics.wallDetected)
        {
            return false;
        }
        return true;
    }
}
