using UnityEngine;

public class BreakMe : MonoBehaviour
{
    [SerializeField] private GrapplinghookScript grapplingScript;
    [SerializeField] private BossFightScript bossFightScript;

    // this script is for David Level 1, wher I want an object to be movable, but then fall through the map, and destroy
    // Simple to use, just drag script on the object that we can move, and then it works :]

    private Collider thisCollider;
    private Rigidbody rb;

    // Start
    void Start()
    {
        thisCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        thisCollider.isTrigger = false;
    }

    // Update
    void Update()
    {
        if (thisCollider.enabled == false)
        {
            rb.isKinematic = false;
            thisCollider.enabled = false;
            rb.useGravity = true;
            thisCollider.isTrigger = true;
        }

        if (transform.position.y < -20f)
        {
            if (grapplingScript.isObjGrapple == true)
            {
                return;
            }

            bossFightScript.boxesDestroyedBoss++;
            Destroy(gameObject);
        }
    }


}
