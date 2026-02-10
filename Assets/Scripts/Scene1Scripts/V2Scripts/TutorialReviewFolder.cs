using UnityEngine;

public class TutorialReviewFolder : MonoBehaviour
{
    [Header("Supervisor Materials")]
    public GameObject smallIDCard;
    public GameObject smallExamTicket;
    public GameObject smallMaterials;

    [Header("Review folder")]
    public GameObject q1YesCheck;
    public GameObject q1NoCheck;

    public GameObject q2YesCheck;
    public GameObject q2NoCheck;

    public GameObject q3YesCheck;
    public GameObject q3NoCheck;

    public GameObject admitCheck;
    public GameObject denyCheck;


    [SerializeField] private SupervisorSpeechManager speechManager;

    public GameObject smallReviewFolder;
    public TutorialClickable smallReviewFolderClickable;


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

        // disable the small review folder and make it unclickable
        smallReviewFolderClickable.SetClickable(false);

        // then, continue with the tutorial
        speechManager.ResumeDialogue();

        //// then, advance the time; NVM do this on the clock-in button instead
        //timeManager.AdvanceTime(10);
        
        // close out of the large folder
        gameObject.SetActive(false);

        // return all items to the supervisor
        smallIDCard.SetActive(false);
        smallExamTicket.SetActive(false);
        smallMaterials.SetActive(false);

        // show the small folder again
        smallReviewFolder.SetActive(true);
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

}
