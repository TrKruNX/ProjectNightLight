using UnityEngine;
using UnityEngine.SceneManagement;

public class DealDmgScript : MonoBehaviour
{
    [SerializeField] private PlayerHealthScript playerHealthScript;
    [SerializeField] private float damageAmount = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealthScript.PlayerDmgTake(damageAmount);
        }
    }
}
