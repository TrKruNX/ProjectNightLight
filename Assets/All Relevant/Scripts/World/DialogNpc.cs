using TMPro;
using UnityEngine;

public class DialogNpc : MonoBehaviour
{
    [TextArea(2, 3)]
    public string[] dialogueLines;

    /*
    [SerializeField] private GameObject npcDialogueBox;
    [SerializeField] private TextMeshProUGUI[] npcDialogueText;

    [SerializeField] private string[] npcDialogue;

    private void UpdateTextDialogue()
    {
        for (int i = 0; i < npcDialogueText.Length && i < npcDialogue.Length; i++)
        {
            npcDialogueText[1].text = npcDialogue[i];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcDialogueBox.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateTextDialogue();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcDialogueBox.SetActive(false);
        }
    }
    */
}
