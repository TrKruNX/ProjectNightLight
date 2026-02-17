using UnityEngine;

public class GrapplinghookScript : MonoBehaviour
{

    [Header("CamLocations")]
    [SerializeField] private GameObject firstPersonLoc;
    [SerializeField] private GameObject thirdPersonLoc;

    public bool isAimGrappling;
    
    
    // Start
    void Start()
    {
        
    }

    // Update
    void Update()
    {
        GrappleLogic();

        if (isAimGrappling == true)
        {
            transform.position = firstPersonLoc.transform.position;
        }
        else
        {
            transform.position = thirdPersonLoc.transform.position;
        }
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
}
