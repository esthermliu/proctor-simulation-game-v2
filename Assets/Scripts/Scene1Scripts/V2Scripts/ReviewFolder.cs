using UnityEngine;

public class ReviewFolder : MonoBehaviour
{
    [Header("Paper parents (small)")]
    public GameObject studentIDPaper;
    public GameObject examRegistrationPaper;
    public GameObject reviewFolder;
    public GameObject examGuide;
    public GameObject materials;
    public GameObject externalTicket;

    [Header("Next Paper parents (small)")]
    public GameObject nextReviewFolderSmall;
    public GameObject nextExamGuideSmall;

    [Header("Paper parents (large)")]
    public GameObject studentIDEnlarged;
    public GameObject examRegistrationEnlarged;
    public GameObject reviewFolderEnlarged;
    public GameObject examGuideEnlarged;
    public GameObject materialsEnlarged;
    public GameObject externalTicketEnlarged;

    [Header("Student")]
    public StudentAnimationController student;

    [Header("Student's top icons")]
    public StudentTopIconsManager studentTopIcons;

    [Header("Correct decision")]
    public bool isValid;

    [Header("Review folder")]
    public GameObject q1YesCheck;
    public GameObject q1NoCheck;
    public GameObject q1NACheck;

    public GameObject q2YesCheck;
    public GameObject q2NoCheck;
    public GameObject q2NACheck;

    public GameObject q3YesCheck;
    public GameObject q3NoCheck;
    public GameObject q3NACheck;

    public GameObject q4YesCheck;
    public GameObject q4NoCheck;
    public GameObject q4NACheck;

    public GameObject admitCheck;
    public GameObject denyCheck;

    private bool decisionMade = false;

    public void Admit()
    {
        if (decisionMade) return;
        decisionMade = true;

        // reset all papers + notifications
        ResetPapers();
        NotificationManager.Instance.ResetIncorrectIcons();

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
            NotificationManager.Instance.ShowIncorrectIcon();

            EventLogger.Log(new GameEvent
            {
                eventTypeEnum = EventType.admission_decision_incorrect,
                description = "Admitted student " + student.name
            });
        }

        // increment notification manager student number to keep track of corresponding incorrect icon
        NotificationManager.Instance.IncrementStudent();

        // play the admit animation at bottom of screen
        student.Admit();

        // play the green character animation at top of screen
        studentTopIcons.ShowGreenCharacter();

        // advance time
        TimeManager.Instance.AdvanceTime();
    }

    public void Deny()
    {
        if (decisionMade) return;
        decisionMade = true;

        // reset all papers + notifications
        ResetPapers();
        NotificationManager.Instance.ResetIncorrectIcons();

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
            NotificationManager.Instance.ShowIncorrectIcon();
            EventLogger.Log(new GameEvent
            {
                eventTypeEnum = EventType.admission_decision_incorrect,
                description = "Denied student " + student.name
            });
        }

        // increment notification manager student number to keep track of corresponding incorrect icon
        NotificationManager.Instance.IncrementStudent();

        // play the deny animation at bottom of screen
        student.Deny();

        // play the red character animation at top of screen
        studentTopIcons.ShowRedCharacter();

        // advance time
        TimeManager.Instance.AdvanceTime();
    }

    //========= Admit/Deny Button OnClick Events =========

    // This is connected to the AdmitZone button
    public void OnAdmitClicked()
    {
        OnCheckboxClick(admitCheck, denyCheck, null);
    }

    // This is connected to the DenyZone button
    public void OnDenyClicked()
    {
        OnCheckboxClick(denyCheck, admitCheck, null);
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


        // NEW: logic for extra N/A check completion (for days 2 and 3)
        if (q1NACheck != null)
        {
            q1Incomplete &= !q1NACheck.activeSelf;
            q2Incomplete &= !q2NACheck.activeSelf;
            q3Incomplete &= !q3NACheck.activeSelf;

            bool q4Incomplete = !q4YesCheck.activeSelf && !q4NoCheck.activeSelf && !q4NACheck.activeSelf;
            if (q4Incomplete)
            {
                return;
            }
        }


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
        OnCheckboxClick(q1YesCheck, q1NoCheck, q1NACheck);
    }

    public void OnQ1NoClicked()
    {
        OnCheckboxClick(q1NoCheck, q1YesCheck, q1NACheck);
    }

    public void OnQ1NAClicked()
    {
        OnCheckboxClick(q1NACheck, q1YesCheck, q1NoCheck);
    }

    //========= Q2 Button OnClick Events =========
    public void OnQ2YesClicked()
    {
        OnCheckboxClick(q2YesCheck, q2NoCheck, q2NACheck);
    }

    public void OnQ2NoClicked()
    {
        OnCheckboxClick(q2NoCheck, q2YesCheck, q2NACheck);
    }

    public void OnQ2NAClicked()
    {
        OnCheckboxClick(q2NACheck, q2YesCheck, q2NoCheck);
    }

    //========= Q3 Button OnClick Events =========
    public void OnQ3YesClicked()
    {
        OnCheckboxClick(q3YesCheck, q3NoCheck, q3NACheck);
    }

    public void OnQ3NoClicked()
    {
        OnCheckboxClick(q3NoCheck, q3YesCheck, q3NACheck);
    }

    public void OnQ3NAClicked()
    {
        OnCheckboxClick(q3NACheck, q3YesCheck, q3NoCheck);
    }

    //========= Q4 Button OnClick Events =========
    public void OnQ4YesClicked()
    {
        OnCheckboxClick(q4YesCheck, q4NoCheck, q4NACheck);
    }

    public void OnQ4NoClicked()
    {
        OnCheckboxClick(q4NoCheck, q4YesCheck, q4NACheck);
    }

    public void OnQ4NAClicked()
    {
        OnCheckboxClick(q4NACheck, q4YesCheck, q4NoCheck);
    }

    //===================================
    //        Helper functions 
    //===================================

    // thisBox: the box being clicked
    // otherBox: the other option not being clicked
    // otherBox2: may be null, the other option not being clicked
    private void OnCheckboxClick(GameObject thisBox, GameObject otherBox, GameObject otherBox2)
    {
        // can always set other box to unchecked (regardless of whether it was checked or not)
        otherBox.SetActive(false);

        if (otherBox2 != null)
        {
            otherBox2.SetActive(false);
        }

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

        // reset external ticket if it exists
        if (externalTicket != null)
        {
            externalTicket.SetActive(false);
            externalTicketEnlarged.SetActive(false);
        }

        // get the next misconduct review sheet and exam guide
        // NOTE: If there isn't another student, keep the latest options
        nextReviewFolderSmall.SetActive(true);
        nextExamGuideSmall.SetActive(true);
    }
}
