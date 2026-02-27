using TMPro;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private TurtorialBools turtBools;

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

    private void Update()
    {
        if (playerObj.transform.position.y <= -30f && turtBools.canEnd == false)
        {
            currentPlayerHealth = 0f;
            ResetPos();
        }
        else if (turtBools.canEnd == true && playerObj.transform.position.y <= -2000f)
        {
            currentPlayerHealth = 0f;
            ResetPos();
        }
    }

    public void PlayerDmgTake(float damageAmount)
    {
        currentPlayerHealth -= damageAmount;
        UpdatePlayerHealthText();

        if (currentPlayerHealth <= 0f)
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
        if (currentPlayerHealth > 0f)
            return;
        
        playerObj.GetComponent<CharacterController>().enabled = false;

        playerObj.position = checkpointPos;

        playerObj.GetComponent<CharacterController>().enabled = true;


        currentPlayerHealth = maxPlayerhealth;
        UpdatePlayerHealthText();
    }
}
