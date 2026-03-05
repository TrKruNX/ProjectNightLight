using UnityEngine;
using System.Collections.Generic;

public class BasicSerialieField_AiMovement_Test : MonoBehaviour
{
    [SerializeField] private Transform playerObj;
    private Transform targetPos;
    [SerializeField] private float enemyMoveSpeed = 5f;
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private float distanceMove = 2f;
    private int currentPatrolIndex = 0;

    private bool canMove = true;
    private bool sawPlayer = false;

    // Update
    void Update()
    {
        Patrol();
    }

    private void SimpleEnemyMove()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(transform.rotation, transform.forward, out hit, ))
        
        if (canMove)
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canMove = false;
            transform.position = Vector3.MoveTowards(transform.position, playerObj.position, 0f * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canMove = true;
        }
    }

    private void Patrol()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 15f))
        {
            if (hit.collider.CompareTag("Player") && sawPlayer == false)
            {
                sawPlayer = true;
                return;
            }
        }

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
            else if (Vector3.Distance(transform.position, targetPos.position) > 4f)
            {
                Debug.Log("HEi");
                ResetPatrol();
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
    }
    
    void ResetPatrol()
    {
        sawPlayer = false;
    }
}
