using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject PauseMenuContainer;

    private void Update()
    {
     if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenuContainer.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }

    public void ResumeGame()
    {
        PauseMenuContainer.SetActive(false);
        Time.timeScale = 1f; //resume normal game time
    }

    public void PauseGame()
    {
        PauseMenuContainer.SetActive(true);
        Time.timeScale = 0f; //pause all normal time-based operations
    }


}
