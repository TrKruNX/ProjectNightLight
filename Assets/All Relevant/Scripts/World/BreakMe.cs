using UnityEngine;

public class BreakMe : MonoBehaviour
{
    // this script is for David Level 1, wher I want an object to be movable, but then fall through the map, and destroy
    // Simple to use, just drag script on the object that we can move, and then it works :]

    private Collider thisCollider;
    private Rigidbody rb;

    // Start
    void Start()
    {
        thisCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        thisCollider.isTrigger = true;
    }

    // Update
    void Update()
    {
        if (thisCollider.enabled == false)
        {
            thisCollider.enabled = false;
            rb.useGravity = true;
        }

        if (transform.position.y < -20f)
        {
            Destroy(gameObject);
        }
    }
}
