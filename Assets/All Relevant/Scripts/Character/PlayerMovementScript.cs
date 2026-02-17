using System.Threading;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GrapplinghookScript grapplingScript;

    [SerializeField] private Transform camHolder;

    [Header("Player Move")]
    [SerializeField] private float moveSpeed = 9f;
    [SerializeField] private float gravityForce = -9.81f;
    [SerializeField] private float jumpForce = 5f;
    public bool isJumping;

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
        // temp reset
        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene(1);
        }

        // Get input from keyboard (WASD or arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal"); // -1 to 1 (left/right)
        float moveZ = Input.GetAxisRaw("Vertical");   // -1 to 1 (back/forward)


        GravityOfPlayer();

        MovePlayerBasedOnCamera(moveX, moveZ);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpMech();
        }
    }

    // write float moveX and float moveZ to let the method use those even though I made them in void update
    private void MovePlayerBasedOnCamera(float moveX, float moveZ)
    {
        // This lets us move the player relative to the camera, not the world. So “W” moves forward in the direction you’re looking, not just the world’s Z-axis.
        Vector3 camForward = camHolder.forward;
        Vector3 camRight = camHolder.right;

        // Removes the verticle component
        // Without this, if your camera is looking up/down, moving forward would also move the player up into the air or down into the ground.
        // Setting y = 0, ensures the player only rotates horizontally
        camForward.y = 0f; // btw the .y here does not mean inspector y axis
        camRight.y = 0f;

        // normalize make movement be 1, making it so, holding w-d, moves player faster
        camForward.Normalize();
        camRight.Normalize();

        // combines player input with camera direction
        Vector3 moveDirection = (camForward * moveZ + camRight * moveX); // press W, moveZ = 1

        // flatMove removes vertical movement for rotation purposes
        // Why? LookRotation rotates the player to face a vector. We don’t want them tilting up/down, so Y = 0.
        Vector3 flatMove = new Vector3(moveDirection.x, 0f, moveDirection.z); // ignore Y for rotation

        // connects moveDirection.y with velocitie's y.
        moveDirection.y = verticalVelocity.y;

        // tell charactercontroller to move in movedirection with the speed
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Rotate player only if there is input
        if (flatMove.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatMove); // gives a rotation that faces the movement direction.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    } 

    private void GravityOfPlayer()
    {

        if (characterController.isGrounded == true && !isJumping)
        {
            verticalVelocity.y = -0.5f;
        }
        else if (characterController.isGrounded == false)
        {
            verticalVelocity.y += gravityForce * Time.deltaTime;
            Debug.Log(verticalVelocity.y);
        }
        else
        {
            isJumping = false;
        }
    }

    private void JumpMech()
    {
        if (characterController.isGrounded == true)
        {
            verticalVelocity.y = jumpForce;
            isJumping = true;
        }
    }
}
