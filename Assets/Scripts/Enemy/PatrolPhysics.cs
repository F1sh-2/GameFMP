using UnityEngine;

public class PatrolPhysics : MonoBehaviour
{
  public Rigidbody2D rb;

    [Header("Detect Ground and Walls")]
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private Transform GroundCheckPoint;
    [SerializeField] private LayerMask whatToDetect;
    public bool groundDetected;
    public bool wallDetected;


    private void FixedUpdate()
    {
        groundDetected = Physics2D.OverlapCircle(GroundCheckPoint.position, checkRadius, whatToDetect);
        wallDetected = Physics2D.OverlapCircle(wallCheckPoint.position, checkRadius, whatToDetect);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheckPoint.position, checkRadius);
        Gizmos.DrawSphere(wallCheckPoint.position, checkRadius);
    }

    public void NegateForces()
    {
        rb.linearVelocity = Vector2 .zero;
    }

}
