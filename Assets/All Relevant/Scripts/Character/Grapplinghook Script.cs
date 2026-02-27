using TMPro;
using UnityEngine;

public class GrapplinghookScript : MonoBehaviour
{
    [SerializeField] private PlayerMovementScript playerMove;
    [SerializeField] private TurtorialBools tutBools;

    [SerializeField] private GameObject crossHair;

    [Header("CamLocations")]
    public Transform firstPersonLoc;
    [SerializeField] private Transform camHolder;

    public Transform playerObj;
    [SerializeField] private Transform playerHand;

    public int grapplesLeft = 1;
    public bool isAimGrappling;
    public bool isGrappling;
    public bool isObjGrapple;

    private Vector3 targetPos;
    private Transform objToMe;
    [SerializeField] private LayerMask grappleToMeLayer;
    [SerializeField] private LayerMask grappleObjToMe;


    private Collider objCollider;
    private Rigidbody objRigidbody;

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

        if (Input.GetMouseButtonDown(1) && isAimGrappling == false && tutBools.canGrapple == true)
        {
            isAimGrappling = true;
        }
        else if (Input.GetMouseButtonUp(1) && isAimGrappling == true)
        {
            isAimGrappling = false;
        }


        if (isGrappling == true)
        {
            playerObj.position = Vector3.Lerp(playerObj.position, targetPos, 50f * Time.deltaTime);
        }

        if (isObjGrapple == true)
        {
            objToMe.position = Vector3.Lerp(objToMe.position, playerHand.position, 15f * Time.deltaTime);
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
                targetPos = Vector2.zero;
            }

            if (isObjGrapple == true)
            {
                isObjGrapple = false;
                crossHair.SetActive(false);
                objCollider.enabled = true;
                objRigidbody.useGravity = true;
            }
            else
            {
                targetPos = Vector3.zero;
            }
        }
    }

    public void RayCastGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(camHolder.position, camHolder.forward, out hit, 30f, grappleToMeLayer))
        {
            targetPos = hit.collider.transform.position;
            isGrappling = true;

            grapplesLeft--;
            crossHair.SetActive(true);
        }

        if (Physics.Raycast(camHolder.position, camHolder.forward, out hit, 30f, grappleObjToMe))
        {
            

            objToMe = hit.collider.transform;
            isObjGrapple = true;

            objCollider = objToMe.GetComponent<Collider>();
            objCollider.enabled = false;

            objRigidbody = objToMe.GetComponent<Rigidbody>();
            objRigidbody.useGravity = false;

            crossHair.SetActive(true);
        }

            Debug.DrawRay(camHolder.position, camHolder.forward * 30f, Color.green);
    }
}
