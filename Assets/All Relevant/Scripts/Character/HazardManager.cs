using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth2 pHealth;

    // Start
    void Start()
    {
        
    }

    // Update
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pHealth.TakeDamage(10f);
        }
    }
}
