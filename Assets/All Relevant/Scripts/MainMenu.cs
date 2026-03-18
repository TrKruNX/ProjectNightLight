using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true; //obvi
        Cursor.lockState = CursorLockMode.Confined; //cursor only moves within game window
    }

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
