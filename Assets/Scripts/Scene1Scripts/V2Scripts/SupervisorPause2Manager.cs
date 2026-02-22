using UnityEngine;

public class SupervisorPause2Manager : MonoBehaviour
{
    [Header("Pause 2 Complete Inspection Button")]
    public GameObject completeInspectionButton;

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

    // keep track of which items have been clicked, so we know when to show the complete inspection button
    private bool examTicketClicked = false;
    private bool IDClicked = false;
    private bool examGuideClicked = false;

    // keep track of when pause 2 is done
    private bool completedPause2 = false;

    public void ExamTicketClicked()
    {
        Debug.Log("ExamTicketClicked");
        examTicketClicked = true;
        CheckAllItemsClicked();
    }

    public void IDCardClicked()
    {
        Debug.Log("IDCardClicked");
        IDClicked = true;
        CheckAllItemsClicked();
    }

    public void ExamGuideClicked()
    {
        Debug.Log("ExamGuideClicked");
        examGuideClicked = true;
        CheckAllItemsClicked();
    }

    // show the button once all items have been clicked
    public void CheckAllItemsClicked()
    {
        // don't show button if no longer in phase 2
        if (completedPause2) return;

        if (examTicketClicked && IDClicked && examGuideClicked)
        {
            completeInspectionButton.SetActive(true);
        }
    }

    // function to be called onclick by the inspection button
    public void CompletePause2()
    {
        if (completedPause2) return;

        completedPause2 = true;

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

        // 4) Hide the complete inspection button
        completeInspectionButton.SetActive(false);

        // continue with dialogue
        speechManager.ResumeDialogue();
    }
}
