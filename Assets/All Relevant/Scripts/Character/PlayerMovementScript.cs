using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] private Transform camHolder;

    [Header("Player Move")]
    private float moveSpeed = 9f;


    private bool isGrounded;

    Rigidbody rb;

    // Start
    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();

        // Basically command to freeze rigidbody in specifiq angles "|" <-- this means "and/or"
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {

        // Get input from keyboard (WASD or arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal"); // -1 to 1 (left/right)
        float moveZ = Input.GetAxisRaw("Vertical");   // -1 to 1 (back/forward)

        // Calculate Move Direction
        Vector3 move = new Vector3(moveX, 0f, moveZ);


        // Move the character
        // Time.deltaTime ensures smooth, consistent movement regardless of frame rate
        transform.Translate(move * moveSpeed * Time.deltaTime);

    }
}
