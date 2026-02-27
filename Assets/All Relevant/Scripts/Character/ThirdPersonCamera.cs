using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GrapplinghookScript grapplingScript;

    [Header("Player Logic")]
    [SerializeField] private Transform playerObj;

    [Header("Ui Logic")]
    [SerializeField] private GameObject crossHair;

    [Header("camLogic")]
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float distance = 10f;
    [SerializeField] private LayerMask defaultLayer;
    [SerializeField] private Camera mainCamera;




    [SerializeField] private float minY = -20f; // lowest camera angle
    [SerializeField] private float maxY = 65f;  // highest camera angle

    private float mouseX;
    private float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        print("Buttons: ");
        print("Jump - Space");
        print("WASD - Move");
        print("R - aimGrapple");
        print("LeftMouseButton + R - Grapple");
        print("V - Reset level");
    }

    void Update()
    {
        
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        if (grapplingScript.isAimGrappling && Input.GetMouseButtonDown(0))
        {
            grapplingScript.RayCastGrapple();
        }
    }

    void LateUpdate()
    {
        if (grapplingScript.isAimGrappling == false && grapplingScript.isGrappling == false && grapplingScript.isObjGrapple == false)
        {
            ThirdPersonLogic();
            crossHair.SetActive(false);
        }
        else
        {
            FirstPersonLogic();
            crossHair.SetActive(true);
        }
    }


    private void ThirdPersonLogic()
    {
        mainCamera.fieldOfView = 80;
        mouseY = Mathf.Clamp(mouseY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Vector3 offset = rotation * Vector3.back * distance;

        transform.position = playerObj.position + offset;
        transform.LookAt(playerObj.position);
    }

    private void FirstPersonLogic()
    {

        mainCamera.fieldOfView = 80;
        mouseY = Mathf.Clamp(mouseY, -85f, 85f);

        // Rotate camera
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.rotation = rotation;

        playerObj.rotation = transform.rotation;
        
        transform.position = grapplingScript.firstPersonLoc.position;
    }

}
