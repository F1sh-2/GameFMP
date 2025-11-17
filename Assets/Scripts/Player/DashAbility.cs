using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class DashAbility : BaseAbility
{
    public InputActionReference dashActionRef;
    [SerializeField] private float dashForce;
    [SerializeField] private float maxDashDuration;
    private float dashTimer;


    private void OnEnable()
    {
        dashActionRef.action.performed += TryToDash;
    }

    private void TryToDash(InputAction.CallbackContext value)
    {
        if (!isPermitted)
        {
            return;
        }
        linkedStateMachine.ChangeState(PlayerStates.State.Dash);
        if (player.facingRight)
        {
           linkedPhysics.rb.linearVelocityX = dashForce;
        }
        else
        {
            linkedPhysics.rb.linearVelocityX = -dashForce;
        }


        dashTimer = maxDashDuration;

    }
 
}
