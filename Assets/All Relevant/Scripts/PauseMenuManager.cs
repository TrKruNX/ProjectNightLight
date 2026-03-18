using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject PauseMenuContainer;

    private void Update()
    {
     if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenuContainer.activeInHierarchy)
            {
                PauseMenuContainer.SetActive(false);
                ResumeGame();
            }
            else
            {
                PauseMenuContainer.SetActive(true);
                PauseGame();
            }
        }

    }

    public void ResumeGame()
    {
        PauseMenuContainer.SetActive(false);
        Time.timeScale = 1f; //resume normal game time
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        PauseMenuContainer.SetActive(true);
        Time.timeScale = 0f; //pause all normal time-based operations
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void Level1Button()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

    public void Level2Button()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }

    public void Level3Button()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1f;
    }


}
