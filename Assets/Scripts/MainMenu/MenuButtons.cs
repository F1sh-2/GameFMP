using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void OnMouse()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OffMouse()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

}
