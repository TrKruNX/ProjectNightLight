using UnityEngine;
using UnityEngine.SceneManagement;

public class TurtorialBools : MonoBehaviour
{
    public bool canJump = false;
    public bool canDoubleJump = false;
    public bool canGrapple = false;
    public bool canEnd = false;
    public bool talkToNpcTutorial = false;

    [SerializeField] private GameObject startTutorial;
    public GameObject talkNpcTutText;

    private float timer;
    private float wasdTimer = 10f;
    private bool hasStarted = false;


    private void Update()
    {
        if (hasStarted == true)
        {
            startTutorial.SetActive(true);

            wasdTimer -= Time.deltaTime;
            if (wasdTimer <= 0f)
            {
                hasStarted = false;
                startTutorial.SetActive(false);
                wasdTimer = 10f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && talkNpcTutText.activeInHierarchy)
        {
            OnDoneButton();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpOn"))
        {
            canJump = true;
        }

        if (other.CompareTag("DoubleJumpOn"))
        {
            canDoubleJump = true;
        }

        if (other.CompareTag("GrappleOn"))
        {
            canGrapple = true;
        }

        if (other.CompareTag("EndTurt"))
        {
            canEnd = true;
        }

        if (other.CompareTag("NoobPlayer"))
        {
            startTutorial.SetActive(true);
        }

        if (other.CompareTag("talkTutorial"))
        {
            if (talkNpcTutText != null && talkToNpcTutorial == false)
            {
                talkToNpcTutorial = true;

                talkNpcTutText.SetActive(true);
                Time.timeScale = 0f;

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    OnDoneButton();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NoobPlayer"))
        {
            startTutorial.SetActive(false);
        }

        if (other.CompareTag("EndTurt"))
        {
            canEnd = true;
        }
    }


    void OnStartTimer()
    {
        timer = wasdTimer;
        hasStarted = true;
    }

    public void OnDoneButton()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        talkNpcTutText.SetActive(false);
    }
}
