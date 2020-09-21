using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    
    public void newGame()
    {
        SceneManager.LoadScene("NewGame");
    }

    public void quitGame()
    {
        Debug.Log("Quit!");

        Application.Quit();
    }

    public void logout()
    {
        SceneManager.LoadScene("LoginMenu");
    }

}
