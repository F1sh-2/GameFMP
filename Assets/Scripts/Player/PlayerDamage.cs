using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public RectTransform hearts;
    private int lives;

    private AudioSource source;
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
        lives = hearts.childCount - 1;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hurt");
            hearts.GetChild(lives).gameObject.SetActive(false);
            source.Play();
            lives -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hurt");
            hearts.GetChild(lives).gameObject.SetActive(false);
            source.Play();
            lives -= 1;
        }
    }
}
