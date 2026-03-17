using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private TurtorialBools turtBools;

    [SerializeField] private Transform playerObj;
    [SerializeField] private float currentPlayerHealth;
    public float maxPlayerhealth = 13f;

    public Vector3 checkpointPos;

    [Header("healthbar")]
    [SerializeField] private Slider slider;

    private void Start()
    {
        currentPlayerHealth = maxPlayerhealth;
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

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void PlayerDmgTake(float damageAmount)
    {
        currentPlayerHealth -= damageAmount;
        
        SetHealth(currentPlayerHealth);

        if (currentPlayerHealth <= 0f)
        {
            ResetPos();
        }
    }

    public void NewMaxHealth(float maxHealth)
    {
        currentPlayerHealth = maxHealth;
    }

    public void ResetPos()
    {
        if (currentPlayerHealth > 0f)
            return;

        playerObj.GetComponent<CharacterController>().enabled = false;

        playerObj.position = checkpointPos;
        SetMaxHealth(maxPlayerhealth);

        playerObj.GetComponent<CharacterController>().enabled = true;


        currentPlayerHealth = maxPlayerhealth;
    }
}
