using TMPro;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private Transform playerObj;
    [SerializeField] private float currentPlayerHealth = 13;
    private float maxPlayerhealth;
    [SerializeField] private TextMeshProUGUI hpLeft;

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
            Debug.Log("1");
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

    private void ResetPos()
    {
        if (currentPlayerHealth > 0)
            return;

        Debug.Log("2");
        playerObj.GetComponent<CharacterController>().enabled = false;
        
        playerObj.position = new Vector3(0f, 3f, 0f);
        
        playerObj.GetComponent<CharacterController>().enabled = true;


        currentPlayerHealth = maxPlayerhealth;
        UpdatePlayerHealthText();
    }
}
