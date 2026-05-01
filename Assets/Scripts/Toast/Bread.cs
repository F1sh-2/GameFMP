using UnityEngine;

public class Bread : MonoBehaviour
{
    public float DestroyDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, DestroyDelay);
    }

   
}
