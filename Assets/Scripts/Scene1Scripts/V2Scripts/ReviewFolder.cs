using UnityEngine;

public class ReviewFolder : MonoBehaviour
{
    [Header("Paper parents (small)")]
    public GameObject studentIDPaper;
    public GameObject examRegistrationPaper;
    public GameObject reviewFolder;
    public GameObject examGuide;
    public GameObject materials;

    [Header("Next Paper parents (small)")]
    public GameObject nextReviewFolderSmall;
    public GameObject nextExamGuideSmall;

    [Header("Paper parents (large)")]
    public GameObject studentIDEnlarged;
    public GameObject examRegistrationEnlarged;
    public GameObject reviewFolderEnlarged;
    public GameObject examGuideEnlarged;
    public GameObject materialsEnlarged;

    [Header("Student")]
    public StudentAnimationController student;

    [Header("Student's top icons")]
    public StudentTopIconsManager studentTopIcons;

    [Header("Correct decision")]
    public bool isValid;

    [Header("Notification manager")]
    public NotificationManager notificationManager;

    [Header("Time manager")]
    public TimeManager timeManager;

    [Header("Review folder")]
    public GameObject q1YesCheck;
    public GameObject q1NoCheck;

    public GameObject q2YesCheck;
    public GameObject q2NoCheck;

    public GameObject q3YesCheck;
    public GameObject q3NoCheck;

    public GameObject admitCheck;
    public GameObject denyCheck;

    private bool decisionMade = false;

    public void Admit()
    {
        if (decisionMade) return;
        decisionMade = true;

        // reset all papers + notifications
        ResetPapers();
        notificationManager.ResetIncorrectIcons();

        // ---- GAME LOGIC ----

        if (isValid)
        {
            // adding the null check so we can run scene 1 on its own
            if (GameManager.Instance != null)
            {
                GameManager.Instance.DecideStudentCorrectly();
            }

            EventLogger.Log(new GameEvent
            {
                eventTypeEnum = EventType.admission_decision_correct,
                description = "Admitted student " + student.name
            });

        }
        else
        {
            // else: incorrect admission (no increment, show incorrect icon)
            notificationManager.ShowIncorrectIcon();

            EventLogger.Log(new GameEvent
            {
                eventTypeEnum = EventType.admission_decision_incorrect,
                description = "Admitted student " + student.name
            });
        }

        // increment notification manager student number to keep track of corresponding incorrect icon
        notificationManager.IncrementStudent();

        // play the admit animation at bottom of screen
        student.Admit();

        // play the green character animation at top of screen
        studentTopIcons.ShowGreenCharacter();

        // advance time
        timeManager.AdvanceTime();
    }

    public void Deny()
    {
        if (decisionMade) return;
        decisionMade = true;

        // reset all papers + notifications
        ResetPapers();
        notificationManager.ResetIncorrectIcons();

        // ---- GAME LOGIC ----
        if (!isValid)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.DecideStudentCorrectly();
            }
            EventLogger.Log(new GameEvent
            {
                eventTypeEnum = EventType.admission_decision_correct,
                description = "Denied student " + student.name
            });
        }
        else
        {
            // else: incorrect admission (no increment, show incorrect icon)
            notificationManager.ShowIncorrectIcon();
            EventLogger.Log(new GameEvent
            {
                eventTypeEnum = EventType.admission_decision_incorrect,
                description = "Denied student " + student.name
            });
        }

        // increment notification manager student number to keep track of corresponding incorrect icon
        notificationManager.IncrementStudent();

        // play the deny animation at bottom of screen
        student.Deny();

        // play the red character animation at top of screen
        studentTopIcons.ShowRedCharacter();

        // advance time
        timeManager.AdvanceTime();
    }

    //========= Admit/Deny Button OnClick Events =========

    // This is connected to the AdmitZone button
    public void OnAdmitClicked()
    {
        OnCheckboxClick(admitCheck, denyCheck);
    }

    // This is connected to the DenyZone button
    public void OnDenyClicked()
    {
        OnCheckboxClick(denyCheck, admitCheck);
    }

    // This is connected to the CompleteZone button
    public void OnCompleteClicked()
    {
        // 1) The review must be complete before the player can complete it
        //      --> Check if both checkmarks are missing in Q1 OR Q2 OR Q3 OR admit/deny
        bool q1Incomplete = !q1YesCheck.activeSelf && !q1NoCheck.activeSelf;
        bool q2Incomplete = !q2YesCheck.activeSelf && !q2NoCheck.activeSelf;
        bool q3Incomplete = !q3YesCheck.activeSelf && !q3NoCheck.activeSelf;
        bool decisionIncomplete = !admitCheck.activeSelf && !denyCheck.activeSelf;
        if (q1Incomplete || q2Incomplete || q3Incomplete || decisionIncomplete)
        {
            return;
        }

        // 2) Review is complete, so allow player to admit or deny student
        //      --> using else-if just to be extra safe a decision has been made
        if (admitCheck.activeSelf)
        {
            Admit();
        }
        else if (denyCheck.activeSelf)
        {
            Deny();
        }
    }

    //========= Q1 Button OnClick Events =========
    public void OnQ1YesClicked()
    {
        OnCheckboxClick(q1YesCheck, q1NoCheck);
    }

    public void OnQ1NoClicked()
    {
        OnCheckboxClick(q1NoCheck, q1YesCheck);
    }

    //========= Q2 Button OnClick Events =========
    public void OnQ2YesClicked()
    {
        OnCheckboxClick(q2YesCheck, q2NoCheck);
    }

    public void OnQ2NoClicked()
    {
        OnCheckboxClick(q2NoCheck, q2YesCheck);
    }

    //========= Q3 Button OnClick Events =========
    public void OnQ3YesClicked()
    {
        OnCheckboxClick(q3YesCheck, q3NoCheck);
    }

    public void OnQ3NoClicked()
    {
        OnCheckboxClick(q3NoCheck, q3YesCheck);
    }

    //===================================
    //        Helper functions 
    //===================================

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
    }


    private void ResetPapers()
    {
        // remove all small items
        studentIDPaper.SetActive(false);
        examRegistrationPaper.SetActive(false);
        materials.SetActive(false);
        examGuide.SetActive(false);
        reviewFolder.SetActive(false);

        // remove all enlarged items
        studentIDEnlarged.SetActive(false);
        examRegistrationEnlarged.SetActive(false);
        reviewFolderEnlarged.SetActive(false);
        examGuideEnlarged.SetActive(false);
        materialsEnlarged.SetActive(false);

        // get the next misconduct review sheet and exam guide
		// NOTE: If there isn't another student, keep the latest options
        nextReviewFolderSmall.SetActive(true);
        nextExamGuideSmall.SetActive(true);

        // TODO: IF the player got the PREVIOUS student wrong, then make sure
        //      that the notification icon AND message for that student are INACTIVE

    }
}
