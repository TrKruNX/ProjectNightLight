using UnityEngine;

public class GrapplinghookScript : MonoBehaviour
{
    [SerializeField] private PlayerMovementScript playerMove;

    [Header("CamLocations")]
    public Transform firstPersonLoc;
    [SerializeField] private Transform camHolder;

    public Transform playerObj;

    public int grapplesLeft = 1;
    public bool isAimGrappling;
    public bool isGrappling;


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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isAimGrappling == false)
            {
                isAimGrappling = true;
                
            }
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            if (isAimGrappling == true)
            {
                isAimGrappling = false;
            }
        }
    }

    public void RayCastGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(camHolder.position, camHolder.forward, out hit, 15f))
        {
            if (hit.collider.CompareTag("GrapplingMe"))
            {
                Vector3 targetPos = hit.collider.transform.position;
                
                
                playerObj.transform.position = Vector3.Lerp(playerObj.transform.position, targetPos, 100f * Time.deltaTime);
               
                
                isGrappling = true;
                playerMove.jumpsLeft = 1;
                grapplesLeft--;

                Debug.Log("Du Treffer");
            }
        }

        Debug.DrawRay(camHolder.position, camHolder.forward * 15f, Color.green);
    }
}
