using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void NewGame()
    {
        SceneManager.LoadScene("LittleKnight");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
