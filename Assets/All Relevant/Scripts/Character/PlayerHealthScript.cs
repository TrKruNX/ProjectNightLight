using TMPro;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private Transform playerObj;
    [SerializeField] private float currentPlayerHealth = 13;
    private float maxPlayerhealth;
    [SerializeField] private TextMeshProUGUI hpLeft;

    public Vector3 checkpointPos;

    private void Start()
    {
        maxPlayerhealth = 13f;
        UpdatePlayerHealthText();
    }
    public void PlayerDmgTake(float damageAmount)
    {
        currentPlayerHealth -= damageAmount;
        UpdatePlayerHealthText();

        if (currentPlayerHealth <= 0)
        {
            ResetPos();
        }
    }

    public void UpdatePlayerHealthText()
    {
        hpLeft.text = "Health: " + currentPlayerHealth.ToString();
    }

    public void NewMaxHealth(float maxHealth)
    {
        currentPlayerHealth = maxHealth;

        UpdatePlayerHealthText();
    }

    public void ResetPos()
    {
        if (currentPlayerHealth > 0 || playerObj.transform.position.y > -25f)
            return;
        
        playerObj.GetComponent<CharacterController>().enabled = false;

        playerObj.position = checkpointPos;

        playerObj.GetComponent<CharacterController>().enabled = true;


        currentPlayerHealth = maxPlayerhealth;
        UpdatePlayerHealthText();
    }
}
