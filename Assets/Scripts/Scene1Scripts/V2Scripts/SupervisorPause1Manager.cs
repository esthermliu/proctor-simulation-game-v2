using UnityEngine;

public class SupervisorPause1Manager : MonoBehaviour
{
    public TutorialClickable smallIDClickable;

    [SerializeField] private SupervisorSpeechManager speechManager;

    private bool completedPause1 = false;

    // called once the ID has been returned, dialogue should resume, and the small ID should no longer be clickable
    public void CompletePause1()
    {
        // make sure that we can only complete pause 1 ONCE
        if (completedPause1) return;

        completedPause1 = true;

        // make small ID no longer clickable
        smallIDClickable.SetClickable(false);

        // resume dialogue
        speechManager.ResumeDialogue();
    }

}
