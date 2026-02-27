using UnityEngine;
using UnityEngine.SceneManagement;

public class DealDmgScript : MonoBehaviour
{
    [SerializeField] private PlayerHealthScript playerHealthScript;

    /*
    Scene currentScene;
    string sceneName;
    

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealthScript.PlayerDmgTake(100f);
        }
    }
}
