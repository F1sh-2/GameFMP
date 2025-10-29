using UnityEngine;

public class PatrolingStateMachine : EnemySimpleStateMachine
{
    [SerializeField] private PatrolPhysics patrolPhysics;


    [Header("IDLE STATE")]
    [SerializeField] private string idleAnimationName;
    [SerializeField] private float minIdleTime;
    [SerializeField] private float maxIdleTime;
    private float idleStateTimer;

    [Header("MOVE STATE")]
    [SerializeField] private string MoveAnimationName;
    [SerializeField] private float speed;
    [SerializeField] private float minMoveTime;
    [SerializeField] private float maxMoveTime;
    [SerializeField] private float mininumTurnDelay;
    private float moveStateTimer;
    private float turnCooldown;

    #region IDLE
    public override void EnterIdle()
    {
        anim.Play(idleAnimationName);
        idleStateTimer = Random.Range(minIdleTime, maxIdleTime);
        patrolPhysics.NegateForces();
    }
    public override void UpdateIdle()
    {
        idleStateTimer -= Time.deltaTime;
        if(idleStateTimer <= 0)
        {
            ChangeState(EnemyState.Move);
        }
    }
    public override void ExitIdle()
    {
        // do something
    }
    #endregion
    #region MOVE
    public override void EnterMove()
    {
        anim.Play(MoveAnimationName);
        moveStateTimer = Random.Range(minMoveTime, maxMoveTime);
        //ForceFlip();
        //speed *= -1;
    }
    public override void UpdateMove()
    {
        moveStateTimer -= Time.deltaTime;
        if(moveStateTimer <= 0)
            ChangeState(EnemyState.Idle);
        if (turnCooldown > 0)
            turnCooldown -= Time.deltaTime;

        if(patrolPhysics.wallDetected || patrolPhysics.groundDetected == false)
        {
            if (turnCooldown > 0)
                return;
            ForceFlip();
            speed *= -1;
            turnCooldown = mininumTurnDelay;
        }
    }
    public override void FixUpdateMove()
    {
        patrolPhysics.rb.linearVelocity = new Vector2(speed, patrolPhysics.rb.linearVelocityY);
    }
    #endregion
}
