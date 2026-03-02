using UnityEngine;

public class SupervisorPause2Manager : MonoBehaviour
{
    [SerializeField] private SupervisorSpeechManager speechManager;

    [Header("Pause 2 objects")]
    public GameObject smallID;
    public GameObject smallExamTicket;
    public GameObject smallExamGuide;

    public GameObject largeID;
    public GameObject largeExamTicket;
    public GameObject largeExamGuide;

    public TutorialClickable smallIDClickable;
    public TutorialClickable smallExamTicketClickable;
    public TutorialClickable smallExamGuideClickable;

    // keep track of which items have been clicked, so we know when to move on
    private bool examTicketReturned = false;
    private bool IDReturned = false;
    private bool examGuideReturned = false;

    // keep track of when pause 2 is done
    private bool completedPause2 = false;


    public void ExamTicketReturned()
    {
        examTicketReturned = true;
        CheckAllItemsReturned();
    }

    public void IDCardReturned()
    {
        IDReturned = true;
        CheckAllItemsReturned();
    }

    public void ExamGuideReturned()
    {
        examGuideReturned = true;
        CheckAllItemsReturned();
    }

    // move dialogue on when all returned
    public void CheckAllItemsReturned()
    {
        // don't complete pause 2 more than once
        if (completedPause2) return;

        if (examTicketReturned && IDReturned && examGuideReturned)
        {
            CompletePause2();
        }
    }

    // function to be called onclick by the inspection button
    public void CompletePause2()
    {
        if (completedPause2) return;

        completedPause2 = true;

        // For new tutorial, some stuff is redundant, but can keep anyway ig (extra check)

        // 1) Hide the enlarged versions of the objects
        largeID.SetActive(false);
        largeExamTicket.SetActive(false);
        largeExamGuide.SetActive(false);

        // 2) Show the small versions
        smallID.SetActive(true);
        smallExamTicket.SetActive(true);
        smallExamGuide.SetActive(true);

        // 3) Ensure that all small versions can no longer be clicked
        smallIDClickable.SetClickable(false);
        smallExamTicketClickable.SetClickable(false);
        smallExamGuideClickable.SetClickable(false);

        // 4) Ensure that pause 2 is no longer ongoing for all small versions
        smallIDClickable.SetPause2(false);
        smallExamTicketClickable.SetPause2(false);
        smallExamGuideClickable.SetPause2(false);

        // continue with dialogue
        speechManager.ResumeDialogue();
    }
}
