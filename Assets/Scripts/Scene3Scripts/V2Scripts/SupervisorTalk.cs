using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SupervisorTalk : MonoBehaviour, IPointerClickHandler
{
    //public GameObject evalSprite;
    //public GameObject endDay;

    //[Header("Performance Evaluation Info")]
    //public int dayNumber = 1;
    //public TMP_Text correctAdmissionsText;
    //public TMP_Text studentsFlaggedText;
    //public TMP_Text studentsReportedText;
    //public TMP_Text studentsHelpedText;
    //public TMP_Text overallAssessmentText;

    [Header("Supervisor")]
    public Animator supervisorAnimator;

    [Header("Dialogue Manager")]
    public DialogueManager dialogueManager;

    // Don't really need to check for double clicking (since sets game object to inactive), but can keep anyway
    private bool clicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        // prevent double clicking
        if (clicked) return;
        clicked = true;

        // Hide talk prompt (script is attached to talk prompt)
        gameObject.SetActive(false);

        // Trigger the start of the dialogue
        dialogueManager.StartDialogue();

        // Since we started talking to the supervisor, trigger the start looking animation
        supervisorAnimator.SetTrigger("StartLooking");

        // TODO: ONLY SHOW THE FORM AFTER THE DIALOGUE ENDS
        // show the form
        //evalSprite.SetActive(true);

        //// show end day sprite
        //endDay.SetActive(true);

        //// Fill in the text from GameManager
        //UpdatePerformanceEvalText();

    }

    //void UpdatePerformanceEvalText()
    //{
    //    // Do not update Paycheck text if no game manager is present
    //    if (GameManager.Instance == null)
    //    {
    //        return;
    //    }

    //    int correctToday = GameManager.Instance.correctToday;
    //    int flaggedToday = GameManager.Instance.flaggedToday;
    //    int reportedToday = GameManager.Instance.reportedToday;
    //    int helpedToday = GameManager.Instance.helpedToday;


    //    // update text
    //    correctAdmissionsText.text = correctToday + "";
    //    studentsFlaggedText.text = flaggedToday + "";
    //    studentsReportedText.text = reportedToday + "";
    //    studentsHelpedText.text = helpedToday + "";

    //    overallAssessmentText.text = OverallAssessment();
    //}

    //private string OverallAssessment()
    //{
    //    if (dayNumber == 1)
    //    {
    //        return OverallAssessmentDay1();
    //    }

    //    return "";
    //}

    //private string OverallAssessmentDay1()
    //{
    //    int correctToday = GameManager.Instance.correctToday;
    //    int flaggedToday = GameManager.Instance.flaggedToday;
    //    int reportedToday = GameManager.Instance.reportedToday;
    //    int helpedToday = GameManager.Instance.helpedToday;

    //    bool satisfactory = correctToday >= 4 && flaggedToday >= 1 && reportedToday == 1 && helpedToday == 1;
    //    bool requiresReview = correctToday >= 3 && flaggedToday >= 1 && reportedToday < 2 && helpedToday == 1;
    //    if (satisfactory)
    //    {
    //        return "SATISFACTORY";
    //    } else if (requiresReview)
    //    {
    //        return "REQUIRES REVIEW";
    //    } else
    //    {
    //        return "INADEQUATE";
    //    }
    //}
}
