using UnityEngine;

public class EyesDetection : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed;
    public float rotationModifier;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle,Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,q,Time.deltaTime * rotationSpeed);
    }
}
