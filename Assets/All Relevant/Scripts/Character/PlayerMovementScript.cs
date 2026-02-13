using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] private Transform camHolder;

    [Header("Player Move")]
    private float moveSpeed = 9f;
    private float gravityForce = -9.81f;

    private Vector3 move;
    private Vector3 verticalVelocity;

    private CharacterController characterController;

    // Start
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update
    private void Update()
    {
        GravityOfPlayer();
    }

    private void FixedUpdate()
    {

        // Get input from keyboard (WASD or arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal"); // -1 to 1 (left/right)
        float moveZ = Input.GetAxisRaw("Vertical");   // -1 to 1 (back/forward)

        // Calculate Move Direction
        move = new Vector3(moveX, 0f, moveZ);


        // Move the character
        // Time.deltaTime ensures smooth, consistent movement regardless of frame rate
        transform.Translate(move * moveSpeed * Time.deltaTime);
        //transform.rotation = camHolder.rotation;
        move.y = verticalVelocity.y;

    }

    private void MovePlayerBasedOnCamera()
    {

    }

    private void GravityOfPlayer()
    {
        // verticalVelocity = Vector3.zero;

        if (characterController.isGrounded == true)
        {
            verticalVelocity.y = -0.5f;
        }
        else
        {
            verticalVelocity.y -= gravityForce * Time.deltaTime;
            Debug.Log(verticalVelocity.y);
        }
    }
}
