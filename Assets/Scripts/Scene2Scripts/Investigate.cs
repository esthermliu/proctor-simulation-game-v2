using UnityEngine;
using UnityEngine.UI;

public class Investigate : MonoBehaviour
{
    // ========== PUBLIC FIELDS ===============

    [Header("Link to investigated student")]
    public Student student;

    [Header("Buttons")]
    public Button investigateYesButton;
    public Button investigateNoButton;

    [Header("Checkmark options")]
    public GameObject investigateYesCheck;
    public GameObject investigateNoCheck;

    public GameObject reportYesCheck;
    public GameObject reportNoCheck;

    [Header("Mark as complete")]
    public GameObject completeZoneHover;

    [Header("Link to Scene 2 Manager")]
    public Scene2Manager scene2Manager;

    // ========== PRIVATE FIELDS ===============

    // keep track of if the investigation question was answered
    private bool investigateAnswered = false;
    private Animator studentAnimator;


    void Start()
    {
        // Get the Animator component from the student
        if (student != null)
        {
            studentAnimator = student.GetComponent<Animator>();
        }
    }


    // ============== BUTTON FUNCTIONS FOR Q1 =============
    public void OnYesClicked()
    {
        // make sure the question wasn't answered already
        if (investigateAnswered) return;
        investigateAnswered = true;

        // Show the red checkmark for the YES option
        OnCheckboxClick(investigateYesCheck, investigateNoCheck);

        // Show the student's explanation
        student.ShowExplanation();

        // IMPORTANT CONSTRAINT: After the question is clicked, the player CANNOT click
        // on either YES or NO anymore for the "Initiate investigation" question
        // Lock both buttons (so no hovering effects or anything if we decide to add them later)
        investigateYesButton.interactable = false;
        investigateNoButton.interactable = false;

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.investigation_initiated,
            elapsedTime = scene2Manager.ElapsedTime,
        });
        
    }

    public void OnNoClicked()
    {
        // make sure the question wasn't answered already
        if (investigateAnswered) return;
        investigateAnswered = true;

        // Show red checkmark for NO optino
        OnCheckboxClick(investigateNoCheck, investigateYesCheck);

        // lock UI for buttons
        investigateYesButton.interactable = false;
        investigateNoButton.interactable = false;

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.investigation_declined,
            elapsedTime = scene2Manager.ElapsedTime,
        });
    }

    // ============== BUTTON FUNCTIONS FOR Q2 =============
    public void OnQ2YesClicked()
    {
        // Show red checkmark for YES option
        OnCheckboxClick(reportYesCheck, reportNoCheck);
    }

    public void OnQ2NoClicked()
    {
        // Show red checkmark for NO option
        OnCheckboxClick(reportNoCheck, reportYesCheck);
    }


    // ============== BUTTON FUNCTIONS FOR COMPLETE REVIEW =============
    public void OnCompleteReviewClicked()
    {
        // check whether all questions are answered; otherwise do not allow button to be clicked
        bool q1Unanswered = (!investigateYesCheck.activeSelf) && (!investigateNoCheck.activeSelf);
        bool q2Unanswered = (!reportYesCheck.activeSelf) && (!reportNoCheck.activeSelf);

        if (q1Unanswered || q2Unanswered) return;

        
      

        // notify Student script if we reported student
        if (reportYesCheck.activeSelf)
        {
            student.ReportedStudent();
        }

        // Check whether the report decision was correct or not
        student.CheckReport();

        // Hide the review folder
        student.HideInvestigate();

        // Event ended, allow player to click on other events
        scene2Manager.ResetOngoingEvent();

        // Trigger the idle animation for the character again
        if (studentAnimator != null)
        {
            studentAnimator.SetTrigger("StartIdle");
        }
    }


    //===================================
    //        Helper functions 
    //===================================

    // Check if all questions have been answered
    // true if all questions answered, false otherwise
    private bool AllQuestionsAnswered()
    {
        // check whether all questions are answered; otherwise do not allow button to be clicked
        bool q1Unanswered = (!investigateYesCheck.activeSelf) && (!investigateNoCheck.activeSelf);
        bool q2Unanswered = (!reportYesCheck.activeSelf) && (!reportNoCheck.activeSelf);

        return !q1Unanswered && !q2Unanswered;
    }

    // Check if all questions answered and we can activate mark complete hover
    private void CheckIfActivateMarkCompleteHover()
    {
        CursorOnHover completeZoneHoverCursor = completeZoneHover.GetComponent<CursorOnHover>();
        HoverHighlight completeZoneHoverHighlight = completeZoneHover.GetComponent<HoverHighlight>();

        if (completeZoneHoverCursor == null || completeZoneHoverHighlight == null) return;

        // Check if all questions answered and can activate hover
        if (AllQuestionsAnswered())
        {
            completeZoneHoverCursor.SetDisableCursorEffect(false);
            completeZoneHoverHighlight.SetDisable(false);
        }
        else
        {
            completeZoneHoverCursor.SetDisableCursorEffect(true);
            completeZoneHoverHighlight.SetDisable(true);
        }
    }

    // thisBox: the box being clicked
    // otherBox: the other option not being clicked
    private void OnCheckboxClick(GameObject thisBox, GameObject otherBox)
    {
        // can always set other box to unchecked (regardless of whether it was checked or not)
        otherBox.SetActive(false);

        // if this box is already checked, set inactive
        if (thisBox.activeSelf)
        {
            thisBox.SetActive(false);
        }
        else
        {
            thisBox.SetActive(true);
        }

        // check if we can activate hover
        CheckIfActivateMarkCompleteHover();
    }


}
