using UnityEngine;

public class Gargoyle : MonoBehaviour
{
    public Rigidbody2D rb;
    public float gravityScale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            rb.gravityScale = gravityScale;
        }
    }
}
