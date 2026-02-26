using UnityEngine;
using UnityEngine.SceneManagement;

public class DealDmgScript : MonoBehaviour
{
    [SerializeField] private PlayerHealthScript playerHealthScript;
    [SerializeField] private LayerMask playerlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("3");
            playerHealthScript.PlayerDmgTake(100f);
        }
    }
}
