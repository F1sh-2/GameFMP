using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    public InputActionReference escapeActionRef;
    public GameObject menu;
    private bool menuVisible = false;

    private void OnEnable()
    {
        if(escapeActionRef != null)
        {
            escapeActionRef.action.performed += Escape;
        }
        
        //escapeActionRef.action.started += Escape;
    }

    private void Escape(InputAction.CallbackContext value)
    {
        Debug.Log("Escape");
        menu.SetActive(menuVisible = !menuVisible);
    }
    public void OnMouse()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OffMouse()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("LittleKnight");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
