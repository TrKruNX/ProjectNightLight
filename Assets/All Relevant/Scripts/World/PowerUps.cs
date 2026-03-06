using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private PlayerHealthScript playerHealthScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChangeMaxHp"))
        {
            playerHealthScript.NewMaxHealth(120f);
        }

        if (other.CompareTag("Checkpoint") || other.CompareTag("CheckpointTurt"))
        {
            playerHealthScript.checkpointPos = other.transform.position;
            playerHealthScript.ResetPos();
        }
    }


}
