using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalLook : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private BossFightScript bossFightScript;

    [SerializeField] private GameObject finalObject;
    bool timerOngoing;
    float timer = 10f;

    [SerializeField] private GameObject tutFinalBoss;
    bool timerStarted;
    float timer2 = 5f;

    private void Start()
    {
        timerTwo();
    }

    void LateUpdate()
    {
        if (bossFightScript.bossDead && finalObject != null)
        {
            Vector3 direction = finalObject.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);

            timerOngoing = true;
        }

        if (timerOngoing == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (timerStarted == true)
        {
            timer2 -= Time.deltaTime;
            tutFinalBoss.SetActive(true);

            if (timer2 <= 0f)
            {
                tutFinalBoss.SetActive(false);
            }
        }
    }

    void timerTwo()
    {
        timerStarted = true;
    }
}
