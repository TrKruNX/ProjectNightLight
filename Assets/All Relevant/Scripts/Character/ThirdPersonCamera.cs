using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GrapplinghookScript grapplingScript;
    [SerializeField] private BossFightScript bossFightScript;

    [Header("Player Logic")]
    [SerializeField] private Transform playerObj;

    [Header("camLogic")]
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float distance = 10f;
    [SerializeField] private LayerMask defaultLayer;
    [SerializeField] private Camera mainCamera;


    [SerializeField] private float minY = -20f; // lowest camera angle
    [SerializeField] private float maxY = 65f;  // highest camera angle

    [SerializeField] private Collider playerCollider;

    [SerializeField] private GameObject finalObject;


    public float camDist = 10f;
    private float currentDistance;

    private float mouseX;
    private float mouseY;

    public bool canBeThirdperson = true;
    private bool isFirstPersonPos = false;

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

        transform.position = transform.position * camDist;
        currentDistance = distance; // Initialize smooth distance
    }

    void Update()
    {

        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        if (grapplingScript.isAimGrappling && Input.GetMouseButtonDown(0))
        {
            grapplingScript.RayCastGrapple();
        }

        if (bossFightScript.bossDead == true)
        {
            transform.LookAt(finalObject.transform.position);
        }
    }

    void LateUpdate()
    {
        // Force FPS if TPS is disabled
        if (!canBeThirdperson && !isFirstPersonPos)
        {
            isFirstPersonPos = true;
        }

        if (!grapplingScript.isAimGrappling && !grapplingScript.isGrappling)
        {
            if (!isFirstPersonPos) // TPS collision logic
                OnCamDIst();
            else // FPS TPS check
                CheckFPSForTPSSwitch();
        }

        // Decide camera mode
        if (isFirstPersonPos || grapplingScript.isAimGrappling || grapplingScript.isGrappling || grapplingScript.isObjGrapple)
        {
            FirstPersonLogic();
            grapplingScript.crossHair.SetActive(true);
        }
        else if (canBeThirdperson)
        {
            ThirdPersonLogic();
            grapplingScript.crossHair.SetActive(false);
        }
    }


    private void ThirdPersonLogic()
    {
        mainCamera.fieldOfView = 80;
        mouseY = Mathf.Clamp(mouseY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);

        // Use the smooth currentDistance instead of raw distance
        Vector3 offset = rotation * Vector3.back * currentDistance;
        Vector3 targetPos = playerObj.position + offset;

        transform.position = targetPos;
        transform.LookAt(playerObj.position);

        isFirstPersonPos = false;
    }

    private void FirstPersonLogic()
    {
        mainCamera.fieldOfView = 80;
        mouseY = Mathf.Clamp(mouseY, -85f, 85f);

        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        transform.rotation = rotation;

        playerObj.rotation = Quaternion.Euler(0, mouseX, 0);

        // Smoothly move camera to first-person position
        if (!isFirstPersonPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, grapplingScript.firstPersonLoc.position, 120f * Time.deltaTime);
            if (Vector3.Distance(transform.position, grapplingScript.firstPersonLoc.position) < 0.05f)
                isFirstPersonPos = true;
        }
        else
        {
            transform.position = grapplingScript.firstPersonLoc.position;
        }
    }

    void OnCamDIst()
    {
        RaycastHit hit;
        float maxDist = 10f;
        float minDist = 1f;
        float camSpeed = 12f;

        float firstPersonThreshold = minDist + 0.05f;
        float thirdPersonThreshold = minDist + 0.3f;

        // Direction from player toward camera (green line)
        Vector3 dirToCamera = (transform.position - playerObj.position).normalized;

        float targetDistance = maxDist;

        if (Physics.Raycast(playerObj.position, dirToCamera, out hit, maxDist, defaultLayer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider != playerCollider) // ignore self
            {
                targetDistance = Mathf.Max(hit.distance - 0.2f, minDist);
            }
        }

        // Smoothly interpolate distance
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, camSpeed * Time.deltaTime);

        // Grappling active if camera very close
        grapplingScript.isAimGrappling = currentDistance <= minDist;

        // Hysteresis to prevent flickering
        if (currentDistance <= firstPersonThreshold)
        {
            isFirstPersonPos = true;
        }
        else if (currentDistance >= thirdPersonThreshold && !grapplingScript.isAimGrappling)
        {
            isFirstPersonPos = false;
        }

        // Debug rays
        Debug.DrawRay(playerObj.position, dirToCamera * maxDist, Color.green); // actual camera detection
        Debug.DrawRay(playerObj.position, -dirToCamera * 10f, Color.yellow);
    }

    void CheckFPSForTPSSwitch()
    {
        // Only attempt to switch if TPS is allowed
        if (!canBeThirdperson)
            return;

        float minTPSDistance = 3f; // threshold for switching to TPS
        Vector3 origin = playerObj.position + Vector3.up * 1.5f; // eye height
        Vector3 backDir = -playerObj.forward;

        RaycastHit hit;
        float targetDistance = distance;

        // Raycast behind the player
        if (Physics.Raycast(origin, backDir, out hit, distance, defaultLayer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider != playerCollider)
                targetDistance = hit.distance - 0.2f;
        }

        // If distance is enough, allow TPS
        if (targetDistance > minTPSDistance)
            isFirstPersonPos = false;
    }
}
