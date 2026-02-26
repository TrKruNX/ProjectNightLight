using TMPro;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private Transform playerObj;
    [SerializeField] private Transform newPos;
    [SerializeField] private float playerHealth = 13;
    private float maxPlayerhealth;
    [SerializeField] private TextMeshProUGUI hpLeft;

    private void Start()
    {
        maxPlayerhealth = 13f;
        UpdatePlayerHealthText();
    }
    public void PlayerDmgTake(float damageAmount)
    { 
        playerHealth -= damageAmount;
        UpdatePlayerHealthText();

        if (playerHealth <= 0)
        {
            Debug.Log("1");
            ResetPos();
        }
    }

    public void UpdatePlayerHealthText()
    {
        hpLeft.text = "Health: " + playerHealth.ToString();
    }

    private void ResetPos()
    {
        if (playerHealth > 0)
            return;

        Debug.Log("2");
        playerObj.position = newPos.position;
        //transform.position = new Vector3(0f, 3f, 0f);
        
        playerHealth = maxPlayerhealth;
        UpdatePlayerHealthText();
    }
}
