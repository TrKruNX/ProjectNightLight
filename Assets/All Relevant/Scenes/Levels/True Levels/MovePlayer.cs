using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private BossFightScript bossFightScript;
    [SerializeField] private PlayerMovementScript playerMoveScript;

    [SerializeField] private GameObject targetPos;
    [SerializeField] private GameObject targetPos2;
    public Transform playerObj;
    [SerializeField] private GameObject invWalls;
    [SerializeField] private GameObject fakeEnemy;
    [SerializeField] private GameObject bossFightBlocks;

    
    public bool moveToMe2;
    public bool canNotMoveAgain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canNotMoveAgain = false;
        playerMoveScript.moveToMe = false;
        moveToMe2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoveScript.moveToMe == true)
        {
            playerObj.transform.position = Vector3.MoveTowards(playerObj.transform.position, targetPos.transform.position, 4500f * Time.deltaTime);
        }
        
        if (moveToMe2 == true)
        {
            playerObj.transform.position = Vector3.MoveTowards(playerObj.transform.position, targetPos2.transform.position, 4500f * Time.deltaTime);
        }

        if (canNotMoveAgain == true)
        {
            invWalls.SetActive(false);
            fakeEnemy.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AngelCollider"))
        {
            if (canNotMoveAgain == false)
            {
                playerMoveScript.moveToMe = true;
                canNotMoveAgain = true;
                Debug.Log("whynotwork");
            }
            else
            {
                moveToMe2 = true;
                bossFightBlocks.SetActive(true);
            }

        }

        if (other.CompareTag("targetPosLvl3"))
        {
            playerMoveScript.moveToMe = false;

            if (moveToMe2 == true)
            {
                moveToMe2 = false;
                bossFightScript.bossOngoing = true;
            }
        }
    }
}
