using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private GameObject targetPos;
    public Transform playerObj;

    public bool moveToMe;
    public bool canNotMoveAgain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canNotMoveAgain = false;
        moveToMe = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToMe == true)
        {
            playerObj.transform.position = Vector3.MoveTowards(playerObj.transform.position, targetPos.transform.position, 100f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AngelCollider") && canNotMoveAgain == false)
        {
            moveToMe = true;
            canNotMoveAgain = true;
            Debug.Log("whynotwork");
        }
    }
}
