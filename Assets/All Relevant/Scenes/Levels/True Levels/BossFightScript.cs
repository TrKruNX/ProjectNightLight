using Unity.VisualScripting;
using UnityEngine;

public class BossFightScript : MonoBehaviour
{
    [SerializeField] private BreakMe breakMe;
    [SerializeField] private GameObject Stage1;
    [SerializeField] private GameObject Stage2;
    [SerializeField] private GameObject Stage3;

    public bool bossOngoing;
    public bool bossDead;

    private bool tutorialStarted;
    private float tutorialTimer = 5f;
    [SerializeField] private GameObject bossTutText;

    public int boxesDestroyedBoss;

    [Header("Voice")]
    public AudioSource bossStart;
    public AudioSource boss3Box;
    public AudioSource boss8Box;
    public AudioSource bossDeath;

    private bool bossStartPlayed;
    private bool boss3BoxPlayed;
    private bool boss8BoxPlayed;
    private bool bossDeathPlayed;

    private void Update()
    {
        if (!bossOngoing) return;

        // --- Start boss fight sound & tutorial text once ---
        if (!bossStartPlayed)
        {
            bossStart?.Play();
            bossStartPlayed = true;

            bossTutText.SetActive(true);
            tutorialStarted = true;
            tutorialTimer = 5f;
        }

        // --- Tutorial countdown ---
        if (tutorialStarted)
        {
            tutorialTimer -= Time.deltaTime;
            if (tutorialTimer <= 0f)
            {
                bossTutText.SetActive(false);
                tutorialStarted = false;
            }
        }

        // --- Stage activation ---
        if (boxesDestroyedBoss == 0)
        {
            Stage1.SetActive(true);
        }
        else if (boxesDestroyedBoss == 3)
        {
            Stage2.SetActive(true);
            Stage1.SetActive(false);
            if (!boss3BoxPlayed)
            {
                boss3Box?.Play();
                boss3BoxPlayed = true;
            }
        }
        else if (boxesDestroyedBoss == 8)
        {
            Stage3.SetActive(true);
            Stage2.SetActive(false);
            if (!boss8BoxPlayed)
            {
                boss8Box?.Play();
                boss8BoxPlayed = true;
            }
        }
        else if (boxesDestroyedBoss == 13)
        {
            Stage3.SetActive(false);
            bossDead = true;
            if (!bossDeathPlayed)
            {
                bossDeath?.Play();
                bossDeathPlayed = true;
            }
        }
    }
}
