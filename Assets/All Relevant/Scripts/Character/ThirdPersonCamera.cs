using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Player Logic")]
    [SerializeField] private Transform playerObj;


    [Header("camLogic")]
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float distance = 10f;
    [SerializeField] private LayerMask defaultLayer;

    
    [SerializeField] private float minY = -20f; // lowest camera angle
    [SerializeField] private float maxY = 65f;  // highest camera angle

    private float mouseX;
    private float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, minY, maxY);

    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Vector3 offset = rotation * Vector3.back * distance;

        transform.position = playerObj.position + offset;
        transform.LookAt(playerObj.position);
    }


    // This doesnt work just yes, but it will be way to prevent clipping in the prototyping
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {
            mouseSensitivity = -200f;
            Debug.Log("Layer Is detected");
        }
        else
        {
            mouseSensitivity = 200f;
        }

        if (collision.gameObject.CompareTag("Default"))
        {
            mouseSensitivity = -200f;
            Debug.Log("Layer Is detected");
        }
        else
        {
            mouseSensitivity = 200f;
        }
    }

}
