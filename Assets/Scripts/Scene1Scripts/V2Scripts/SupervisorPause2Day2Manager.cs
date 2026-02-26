using UnityEngine;

public class SupervisorPause2Day2Manager : MonoBehaviour
{
    public TutorialClickable smallExternalTicketClickable;
    public TutorialClickable smallIDClickable;

    [Header("Link to enlarged ID")]
    public GameObject smallID;
    public GameObject largeID;

    [SerializeField] private SupervisorSpeechManager speechManager;

    private bool completedPause2 = false;

    // called once the ID has been returned, dialogue should resume, and the small ID should no longer be clickable
    public void CompletePause2()
    {
        // make sure that we can only complete pause 2 ONCE
        if (completedPause2) return;

        completedPause2 = true;

        // Make sure to hide the enlarged ID (in case it was clicked) and hide small version
        largeID.SetActive(false);
        smallID.SetActive(false);

        // make small items no longer clickable
        smallIDClickable.SetClickable(false);
        smallExternalTicketClickable.SetClickable(false);

        // resume dialogue
        speechManager.ResumeDialogue();
    }

}
