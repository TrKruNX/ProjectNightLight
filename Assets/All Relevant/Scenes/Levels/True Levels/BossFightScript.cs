using UnityEngine;

public class BossFightScript : MonoBehaviour
{
    [SerializeField] private BreakMe breakMe;
    [SerializeField] private GameObject Stage1;
    [SerializeField] private GameObject Stage2;
    [SerializeField] private GameObject Stage3;

    public bool bossOngoing;
    public bool bossDead;

    public int boxesDestroyedBoss;

    [Header("Voice")]
    public AudioSource bossStart;
    public AudioSource boss3Box;
    public AudioSource boss8Box;
    public AudioSource bossDeath;


    private void Start()
    {
        bossOngoing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossOngoing)
        {
            if (bossStart != null)
            {
                bossStart.Play();
            }

            if (boxesDestroyedBoss == 0)
            {
                Stage1.SetActive(true);
            }


            if (boxesDestroyedBoss == 3)
            {
                if (boss3Box != null)
                {
                    boss3Box.Play();
                }

                Stage2.SetActive(true);
                Stage1.SetActive(false);
            }

            if (boxesDestroyedBoss == 8)
            {
                if (boss8Box != null)
                {
                    boss8Box.Play();
                }

                Stage3.SetActive(true);
                Stage2.SetActive(false);
            }
            
            if (boxesDestroyedBoss == 13)
            {
                Stage3.SetActive(false);
                bossDead = true;

                if (bossDeath != null)
                {
                    bossDeath.Play();
                }
            }

        }
    }
}
