using UnityEngine;

public class PlayerHealth2 : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth = 100f;

    // Start
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0f)
        {

        }
    }

    public void MaxHealthChange(float newHealth)
    {
        maxHealth += newHealth;
        currentHealth = maxHealth;
    }
}
