using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurroundingDetect : MonoBehaviour
{
    [SerializeField] private PlayerHealthScript playerHealthScript;
    [SerializeField] private ThirdPersonCamera thirdPerson;
    [SerializeField] private TurtorialBools tutBools;

    [Header("UI")]
    public GameObject dialoguePanel;  // Assign in Inspector
    public TMP_Text dialogueText;     // Assign in Inspector
    public GameObject pressEText;     // "Press E" prompt

    private GameObject currentNPC;    // The NPC we're near
    private DialogNpc npcDialogue;  // Dialogue data for that NPC
    private int lineIndex = 0; // Tracks which line of dialogue we are on
    private bool dialogueActive = false; // True if currently talking

    private Scene currentScene;
    private int buildIndex;
    private int nextScene;

    [Header("nulcoks")]
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private GameObject notificationVisible;
    private float timer;
    private float timerLenght = 2f;
    private bool isTimerActive = false;


    private void Awake()
    {
        // Create a temporary reference to the current scene.
        currentScene = SceneManager.GetActiveScene();

        // You can then access properties like the scene's name or build index:
        buildIndex = currentScene.buildIndex;
        nextScene = currentScene.buildIndex + 1;


        if (buildIndex != 0)
        {
            MakeTutorialBoolsTrue();
        }
    }
    void Start()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (pressEText != null) pressEText.SetActive(false);

        notificationVisible.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Npcs Talk"))
        {
            currentNPC = other.gameObject;
            npcDialogue = currentNPC.GetComponent<DialogNpc>();

            if (pressEText != null)
            {
                pressEText.SetActive(true);
            }
        }


        if (other.CompareTag("TutorialEnd"))
        {
            if ( nextScene < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextScene);
            }
        }

        if (other.CompareTag("NextLevel"))
        {
            if (nextScene < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextScene);
            }
        }

        // ----- Unlocks/Losts ----- //
        TextNotifications(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Npcs Talk") && other.gameObject == currentNPC)
        {
            currentNPC = null;
            npcDialogue = null;

            if (pressEText != null)
                pressEText.SetActive(false);

            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);

            dialogueActive = false;
        }
    }


    void Update()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!tutBools.talkNpcTutText.activeInHierarchy)
            {
                if (!dialogueActive)
                {
                    StartDialogue();
                }
                else
                {
                    NextLine();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SceneManager.LoadScene(currentScene.buildIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SceneManager.LoadScene(nextScene);
        }

        if (isTimerActive == true)
        {
            notificationVisible.SetActive(true);

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                notificationVisible.SetActive(false);
                isTimerActive = false;
            }
        }
    }

    void StartDialogue()
    {
        if (npcDialogue == null) 
            return;

        // Hide "Press E" text as soon as dialogue starts
        if (pressEText != null)
            pressEText.SetActive(false);

        dialogueActive = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        dialogueText.text = npcDialogue.dialogueLines[lineIndex];
    }

    void NextLine()
    {
        if (npcDialogue == null) 
            return;

        lineIndex++;
        if (lineIndex < npcDialogue.dialogueLines.Length)
        {
            dialogueText.text = npcDialogue.dialogueLines[lineIndex]; // next line
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueActive = false;
        }
    }

    void MakeTutorialBoolsTrue()
    {
        tutBools.canDoubleJump = true;
        tutBools.canJump = true;
        tutBools.canGrapple = true;
        thirdPerson.canBeThirdperson = true;
    }

    void TimerLogic()
    {
        timer = timerLenght;
        isTimerActive = true;
    }

    void TextNotifications(Collider other)
    {
        // UNLOCK AND LOSE THIRD PERSON
        if (other.CompareTag("UnlockThirdPerson") && thirdPerson.canBeThirdperson == false)
        {
            thirdPerson.canBeThirdperson = true;
            notificationText.text = "Third Person Unlocked ";
            TimerLogic();
        }
        else if (other.CompareTag("LostThirdPerson") && thirdPerson.canBeThirdperson == true)
        {
            thirdPerson.canBeThirdperson = false;
            notificationText.text = "Third Person Disabled ";
            TimerLogic();
        }

        // UNLOCK AND LOSE JUMP
        if (other.CompareTag("JumpOn") && tutBools.canJump == false)
        {
            tutBools.canJump = true;
            notificationText.text = "Gained Jump ";
            TimerLogic();
        }
        else if (other.CompareTag("LostJump") && tutBools.canJump == true)
        {
            tutBools.canJump = false;
            notificationText.text = "Jump Lost ";
            TimerLogic();
        }

        // UNLOCK AND LOSE DOUBLE JUMP
        if (other.CompareTag("DoubleJumpOn") && tutBools.canDoubleJump == false)
        {
            tutBools.canDoubleJump = true;
            notificationText.text = "Gained Double Jump ";
            TimerLogic();
        }
        else if (other.CompareTag("LostDoubleJump") && tutBools.canDoubleJump == true)
        {
            tutBools.canDoubleJump = false;
            notificationText.text = "Double Jump Lost ";
            TimerLogic();
        }

        // UNLOCK AND LOSE GRAPPLE
        if (other.CompareTag("GrappleOn") && tutBools.canGrapple == false)
        {
            tutBools.canGrapple = true;
            notificationText.text = "Gained Grappling ";
            TimerLogic();

            // Debug.Log(other.name + " triggered grapple"); // good debug log, to find name of object
        }
        else if (other.CompareTag("LostGrapple") && tutBools.canGrapple == true)
        {
            tutBools.canGrapple = false;
            notificationText.text = "Lost Grappling ";
            TimerLogic();
        }

        // UNLOCK AND LOSE ALL
        if (other.CompareTag("LostAll"))
        {
            tutBools.canDoubleJump = false;
            tutBools.canJump = false;
            tutBools.canGrapple = false;
            notificationText.text = "Lost All Mechanics ";
            TimerLogic();
        }
        else if (other.CompareTag("GainAll"))
        {
            MakeTutorialBoolsTrue();
            notificationText.text = "Gained All Mechs ";
            TimerLogic();
        }
    }
}
