using TMPro;
using UnityEngine;

public class GrapplinghookScript : MonoBehaviour
{
    [SerializeField] private PlayerMovementScript playerMove;
    [SerializeField] private TurtorialBools tutBools;

    [SerializeField] private GameObject crossHairActive;

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
    [SerializeField] private LayerMask blockGrappleRaycast;
    [SerializeField] private LayerMask raycastOptions;

    public float timer;
    private float raycastTimer = 0.4f;
    private bool isRayTime = false;

    private Collider objCollider;
    private Rigidbody objRigidbody;

    [SerializeField] private Collider playerCollider;

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
                crossHairActive.SetActive(true);
            }
            else
            {
                crossHairActive.SetActive(false);
            }
        }
        else
        {
            crossHairActive.SetActive(false);
        }


        if (isGrappling == true)
        {
            playerObj.position = Vector3.Lerp(playerObj.position, targetPos, 30f * Time.deltaTime);
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

    public void RayCastGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(camHolder.position, camHolder.forward, out hit, 30f, blockGrappleRaycast))
        {
            int hitLayer = hit.collider.gameObject.layer;
            
            if (hitLayer == LayerMask.NameToLayer("GrappleToMeLayer"))
            {
                targetPos = hit.point;
                isGrappling = true;

                grapplesLeft--;
            }

            // this makes it so, if it is a wall layer/defualt layer, tehn do nothing
            // or simpler, do something if it hits any of the grapple layers first
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

                objRigidbody = objToMe.GetComponent<Rigidbody>();
                objRigidbody.useGravity = false;

                grapplesLeft--;
            }
        }

            /*
             * this was a previous code that had a bug where I could go through walls, when grappling, or I could have a wall between
             * me and object, and still grapple
            if (Physics.SphereCast(camHolder.position, sphereRadius, camHolder.forward, out hit, 30f, grappleToMeLayer))
            {
                targetPos = hit.collider.transform.position;
                isGrappling = true;

                grapplesLeft--;
                crossHair.SetActive(true);
            }

            if (Physics.SphereCast(camHolder.position, sphereRadius, camHolder.forward, out hit, 30f, grappleObjToMe))
            {
                objToMe = hit.collider.transform;
                isObjGrapple = true;

                objCollider = objToMe.GetComponent<Collider>();
                objCollider.enabled = false;

                objRigidbody = objToMe.GetComponent<Rigidbody>();
                objRigidbody.useGravity = false;

                crossHair.SetActive(true);
            }
            */

            Debug.DrawRay(camHolder.position, camHolder.forward * 30f, Color.green);
    }

    void RayTimerOn()
    {
        timer = raycastTimer;
        isRayTime = true;
    }
}
