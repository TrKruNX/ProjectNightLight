using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
