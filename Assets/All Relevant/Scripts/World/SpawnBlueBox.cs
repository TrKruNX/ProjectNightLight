using System.Linq;
using UnityEngine;

public class SpawnBlueBox : MonoBehaviour
{
    [SerializeField] private GameObject blueBoxPrefab;
    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private Transform camHolder;

    // Update
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(camHolder.position, camHolder.forward, out hit, 15f))
        {
            if (hit.collider.CompareTag("ButtonSpawnBlue") && Input.GetMouseButtonDown(0))
            {
                Instantiate(blueBoxPrefab, spawnLocation.transform.position, transform.rotation);
            }
        }
    }
}
