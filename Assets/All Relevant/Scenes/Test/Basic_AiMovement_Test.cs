using UnityEngine;
using System.Collections.Generic;

public class Basic_AiMovement_Test : MonoBehaviour
{
    private EnemyHealth aiMove;

    [SerializeField] private Transform playerObj;
    private Transform targetPos;
    [SerializeField] private float enemyMoveSpeed = 5f;
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private float distanceMove = 2f;
    private int currentPatrolIndex = 0;

    private bool canMove = true;
    public bool sawPlayer = false;


    private void Start()
    {
        aiMove = GetComponent<EnemyHealth>();
    }

    // Update
    void Update()
    {
        Patrol();
    }

    private void SimpleEnemyMove()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(transform.rotation, transform.forward, out hit, ))
        
        if (canMove == true)
        {

            if (sawPlayer == false)
            {
                // move towards player
                transform.position = Vector3.MoveTowards(transform.position, targetPos.position, enemyMoveSpeed * Time.deltaTime);

                // Look at player
                transform.LookAt(targetPos.position);
            }
            else
            {
                // move towards player
                transform.position = Vector3.MoveTowards(transform.position, playerObj.position, enemyMoveSpeed * Time.deltaTime);

                // Look at player
                transform.LookAt(playerObj.position);
            }

        }
    }

    private void Patrol()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 15f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) 
        {
            if (hit.collider.CompareTag("Player") && sawPlayer == false)
            {
                sawPlayer = true;
            }
        }
        Debug.DrawRay(transform.position, transform.forward * 15f, Color.green);

        if (sawPlayer == false)
        {
            targetPos = patrolPoints[currentPatrolIndex];
            SimpleEnemyMove();

            if (Vector3.Distance(transform.position, targetPos.position) < distanceMove)
            {
                currentPatrolIndex++;

                if (currentPatrolIndex >= patrolPoints.Count)
                {
                    currentPatrolIndex = 0;
                }

            }
        }
        else
        {
            Chase();
        }
    }

    void Chase()
    {
        SimpleEnemyMove();

        targetPos = playerObj;
        
        if (Vector3.Distance(transform.position, targetPos.position) > 10f && sawPlayer == true)
        {
            ResetPatrol();
        }
        
    }
    
    void ResetPatrol()
    {
        sawPlayer = false;
        aiMove.TakeDamage(5f);
        targetPos = patrolPoints[currentPatrolIndex];
    }
}
