using TMPro;
using UnityEngine;

public class SurroundingDetect : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;  // Assign in Inspector
    public TMP_Text dialogueText;     // Assign in Inspector
    public GameObject pressEText;     // "Press E" prompt

    private GameObject pickupable;


    private GameObject currentNPC;    // The NPC we're near
    private DialogNpc npcDialogue;  // Dialogue data for that NPC
    private int lineIndex = 0; // Tracks which line of dialogue we are on
    private bool dialogueActive = false; // True if currently talking


    void Start()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (pressEText != null) pressEText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewRespawnLoc"))
        {
            currentNPC = other.gameObject;
            npcDialogue = currentNPC.GetComponent<DialogNpc>();

            if (pressEText != null)
            {
                pressEText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NewRespawnLoc") && other.gameObject == currentNPC)
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
}
