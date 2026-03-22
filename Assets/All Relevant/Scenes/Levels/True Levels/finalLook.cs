using UnityEngine;

public class finalLook : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private BossFightScript bossFightScript;

    [SerializeField] private GameObject finalObject;


    void Update()
    {
        if (bossFightScript.bossDead == true)
        {
            transform.LookAt(finalObject.transform.position);
        }
    }
}
