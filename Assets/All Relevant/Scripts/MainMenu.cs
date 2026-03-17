using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        //load the game scene
        SceneManager.LoadScene(1); //game scene here heheh
    }

    public void QuitGame()
    {
        //quit the game
        Application.Quit();

    }
  
}
