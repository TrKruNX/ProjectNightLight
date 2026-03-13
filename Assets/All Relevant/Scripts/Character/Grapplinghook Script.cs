using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplinghookScript : MonoBehaviour
{
    [SerializeField] private PlayerMovementScript playerMove;
    [SerializeField] private TurtorialBools tutBools;
    [SerializeField] private ThirdPersonCamera thirdPersonScript;

    [Header("Ui Logic")]
    public GameObject crossHair;
    [SerializeField] private GameObject crossHairActive_Blue;
    [SerializeField] private GameObject crossHairActive_Red;

    [Header("CamLocations")]
    public Transform firstPersonLoc;
    [SerializeField] private Transform camHolder;
    [SerializeField] private float sphereCastRadius;

    public Transform playerObj;
    [SerializeField] private Transform playerHand;

    public int grapplesLeft = 1;
    public bool isAimGrappling;
    public bool isGrappling;
    public bool isObjGrapple;
    public bool forceElse = false;

    private Vector3 targetPos;
    private Transform objToMe;
    [SerializeField] private LayerMask blockGrappleRaycast;
    [SerializeField] private LayerMask raycastOptions;

    public float timer;
    private float raycastTimer = 1f;
    private bool isRayTime = false;

    private Collider objCollider;
    private Rigidbody objRigidbody;

    private bool throwObj = false;

    [SerializeField] private Collider playerCollider;
    [SerializeField] private LayerMask targetLayerMask;

    // Update
    void Update()
    {
        GrappleLogic();

        if (isRayTime == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                isRayTime = false;
            }
        }
    }

    private void GrappleLogic()
    {
        if (isAimGrappling == true)
        {
            thirdPersonScript.camDist = 0f;
        }
        

        if (Input.GetMouseButtonDown(1) && isAimGrappling == false && tutBools.canGrapple == true)
        {
            isAimGrappling = true;
        }
        else if (Input.GetMouseButtonUp(1) && isAimGrappling == true)
        {
            isAimGrappling = false;
        }

        if (isAimGrappling == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(camHolder.position, camHolder.forward, out hit, 30f, raycastOptions))
            {
                int hitLayer = hit.collider.gameObject.layer;
                
                if (hitLayer == LayerMask.NameToLayer("GrappleToLayer"))
                {
                    crossHairActive_Blue.SetActive(true);
                    
                }

                if (hitLayer == LayerMask.NameToLayer("GrappleObjToMe"))
                {
                    crossHairActive_Red.SetActive(true);
                }
            }
            else
            {
                crossHairActive_Blue.SetActive(false);
                crossHairActive_Red.SetActive(false);

            }
        }
        else
        {
            crossHairActive_Blue.SetActive(false);
            crossHairActive_Red.SetActive(false);
        }


        if (isGrappling == true)
        {
            playerObj.position = Vector3.MoveTowards(playerObj.position, targetPos, 120f * Time.deltaTime);
        }

        if (isObjGrapple == true)
        {
            objToMe.position = Vector3.MoveTowards(objToMe.position, playerHand.position, 60f * Time.deltaTime);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                objRigidbody.AddForce(camHolder.forward * 15f, ForceMode.Impulse);
            }
        }

        if (Input.GetMouseButtonUp(0) || isObjGrapple == true && Input.GetKeyDown(KeyCode.E))
        {
            if (isGrappling == true)
            {
                isGrappling = false;
            }
            else
            {
                targetPos = Vector2.zero;
            }

            if (isObjGrapple == true)
            {
                isObjGrapple = false;

                objCollider.enabled = true;
                objRigidbody.useGravity = true;
            }
            else
            {
                targetPos = Vector3.zero;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (objToMe && collision.gameObject.layer == 0)
        {
            objRigidbody.useGravity = false;
            objRigidbody.isKinematic = true;
        }
    }


    public void RayCastGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(camHolder.position, camHolder.forward, out hit, 30f, blockGrappleRaycast))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == LayerMask.NameToLayer("GrappleToLayer"))
            {
                targetPos = hit.point;
                isGrappling = true;

                grapplesLeft--;
            }
        }

        if (Physics.Raycast(camHolder.position, camHolder.forward, out hit, 30f, blockGrappleRaycast))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == LayerMask.NameToLayer("GrappleObjToMe") && isRayTime == false)
            {


                RayTimerOn();

                objToMe = hit.collider.transform;
                isObjGrapple = true;

                objCollider = objToMe.GetComponent<Collider>();
                objCollider.enabled = false;
                //Physics.IgnoreCollision(objCollider, playerCollider, true);

                objRigidbody = objToMe.GetComponent<Rigidbody>();
                objRigidbody.useGravity = false;

                grapplesLeft--;
            }
        }

        if (Physics.SphereCast(camHolder.position, sphereCastRadius, camHolder.forward, out hit, 30f, blockGrappleRaycast))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == LayerMask.NameToLayer("Default"))
            {
                return;
            }

            if (hitLayer == LayerMask.NameToLayer("GrappleToLayer"))
            {
                targetPos = hit.transform.position;
                isGrappling = true;

                grapplesLeft--;
            }

        }

        if (Physics.SphereCast(camHolder.position, sphereCastRadius, camHolder.forward, out hit, 30f, blockGrappleRaycast))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == LayerMask.NameToLayer("Default"))
            {
                return;
            }

            if (hitLayer == LayerMask.NameToLayer("GrappleObjToMe") && isRayTime == false)
            {
                RayTimerOn();

                objToMe = hit.collider.transform;
                isObjGrapple = true;

                objCollider = objToMe.GetComponent<Collider>();
                objCollider.enabled = false;
                //Physics.IgnoreCollision(objCollider, playerCollider, true);

                objRigidbody = objToMe.GetComponent<Rigidbody>();
                objRigidbody.useGravity = false;

                grapplesLeft--;
            }
        }
        Debug.DrawRay(camHolder.position, camHolder.forward * 30f, Color.green);
    }

    void RayTimerOn()
    {
        timer = raycastTimer;
        isRayTime = true;
    }
}
