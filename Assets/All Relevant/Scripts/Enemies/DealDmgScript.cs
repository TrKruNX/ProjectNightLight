using UnityEngine;
using UnityEngine.SceneManagement;

public class DealDmgScript : MonoBehaviour
{
    [SerializeField] private PlayerHealthScript playerHealthScript;
    [SerializeField] private float damageAmount = 100f;

    [SerializeField] private GameObject deathMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealthScript.PlayerDmgTake(damageAmount);

            deathMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void deatMenuRetry()
    {
        Time.timeScale = 1f;
        deathMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
