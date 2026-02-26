using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private PlayerHealthScript playerHealthScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChangeMaxHp"))
        {
            playerHealthScript.NewMaxHealth(120f);
        }
    }


}
