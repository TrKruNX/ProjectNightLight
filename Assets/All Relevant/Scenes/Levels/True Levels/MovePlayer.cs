using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private BossFightScript bossFightScript;

    [SerializeField] private GameObject targetPos;
    [SerializeField] private GameObject targetPos2;
    public Transform playerObj;
    [SerializeField] private GameObject invWalls;
    [SerializeField] private GameObject fakeEnemy;
    [SerializeField] private GameObject bossFightBlocks;

    public AudioSource bossPush;

    public bool moveToMe;
    public bool moveToMe2;
    public bool canNotMoveAgain;

    private CharacterController controller;

    // Start
    void Start()
    {
        controller = playerObj.GetComponent<CharacterController>();

        canNotMoveAgain = false;
        moveToMe = false;
        moveToMe2 = false;
    }

    // Update
    void Update()
    {
        if (moveToMe == true)
        {
            Vector3 direction = (targetPos.transform.position - playerObj.position).normalized;
            controller.Move(direction * 600f * Time.deltaTime);
        }
        
        if (moveToMe2 == true)
        {
            Vector3 direction = (targetPos2.transform.position - playerObj.position).normalized;
            controller.Move(direction * 500f * Time.deltaTime);
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
                moveToMe = true;
                canNotMoveAgain = true;

                if (bossPush != null)
                {
                    bossPush.Play();
                }
            }
            else
            {
                moveToMe2 = true;
                bossFightBlocks.SetActive(true);
            }

        }

        if (other.CompareTag("targetPosLvl3"))
        {
            moveToMe = false;

            if (moveToMe2 == true)
            {
                moveToMe2 = false;
                bossFightScript.bossOngoing = true;
            }
        }
    }
}
