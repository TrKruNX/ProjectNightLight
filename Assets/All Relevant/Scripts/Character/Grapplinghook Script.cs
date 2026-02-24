using TMPro;
using UnityEngine;

public class GrapplinghookScript : MonoBehaviour
{
    [SerializeField] private PlayerMovementScript playerMove;

    [SerializeField] private GameObject crossHair;

    [Header("CamLocations")]
    public Transform firstPersonLoc;
    [SerializeField] private Transform camHolder;

    public Transform playerObj;

    public int grapplesLeft = 1;
    public bool isAimGrappling;
    public bool isGrappling;

    private Vector3 targetPos;
    [SerializeField] private LayerMask grappleToMeLayer;


    // Start
    void Start()
    {
        
    }

    // Update
    void Update()
    {
        GrappleLogic();
    }

    private void GrappleLogic()
    {

        if (Input.GetMouseButtonDown(1) && isAimGrappling == false)
        {
            isAimGrappling = true;
        }
        else if (Input.GetMouseButtonUp(1) && isAimGrappling == true)
        {
            isAimGrappling = false;
        }


        if (isGrappling == true)
        {
            playerObj.position = Vector3.Lerp(playerObj.position, targetPos, 75f * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isGrappling == true)
            {
                isGrappling = false;
                crossHair.SetActive(false);
            }
            else
            {
                targetPos = new Vector3(0f, 0f, 0f);
            }
        }
    }

    public void RayCastGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(camHolder.position, camHolder.forward, out hit, 20f, grappleToMeLayer))
        {
            targetPos = hit.collider.transform.position;
            isGrappling = true;

            grapplesLeft--;
            crossHair.SetActive(true);
        }

            Debug.DrawRay(camHolder.position, camHolder.forward * 20f, Color.green);
    }
}
