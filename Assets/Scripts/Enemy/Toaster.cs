using System.Collections;
using UnityEngine;

public class Toaster : MonoBehaviour
{
    public float toastRate;
    public GameObject toast;
    public Transform direction;
    public float throwForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MakeToast());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MakeToast()
    {
        yield return new WaitForSeconds(toastRate);
        GameObject clone = Instantiate(toast, transform.position, Quaternion.identity);
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.AddForce((direction.position - transform.position) * throwForce, ForceMode2D.Impulse);
        rb.AddTorque(throwForce, ForceMode2D.Force);
        StartCoroutine(MakeToast());
    }
}
