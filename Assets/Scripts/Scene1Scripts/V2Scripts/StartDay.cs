using UnityEngine;

public class StartDay : MonoBehaviour
{
    [Header("Supervisor's items")]
    public GameObject smallReviewFolder;
    public GameObject smallExamGuide;

    [Header("First character's items")]
    public GameObject firstStudent;
    public GameObject nextSmallReviewFolder;
    public GameObject nextSmallExamGuide;

    [Header("Clock In Button")]
    public GameObject clockInButton;

    [Header("Time manager")]
    public TimeManager timeManager;

    [Header("Supervisor top character")]
    public Animator supervisorTopCharacter;

    public void OnClockIn()
    {
        // this will end the tutorial and start the day

        // show the first character's small items and hide the current small items
        smallReviewFolder.SetActive(false);
        smallExamGuide.SetActive(false);

        nextSmallReviewFolder.SetActive(true);
        nextSmallExamGuide.SetActive(true);

        // then, advance time forward by 10 minutes
        timeManager.AdvanceTime(10);

        // then, animate supervisor leaving to the right
        gameObject.GetComponent<Animator>().SetTrigger("SupervisorExit");

        // Animate the little top supervisor character leaving to right as well
        supervisorTopCharacter.SetTrigger("SupervisorExit");

        // Hide the clock-in button
        clockInButton.SetActive(false);
    }

    // Called when supervisor should just exit after the conversation ends
    public void SupervisorExit()
    {
        // then, animate supervisor leaving to the right
        gameObject.GetComponent<Animator>().SetTrigger("SupervisorExit");

        // Animate the little top supervisor character leaving to right as well
        supervisorTopCharacter.SetTrigger("SupervisorExit");
    }

    // CALLED BY ANIMATION EVENT TO ACTIVATE THE FIRST STUDENT
    public void OnSupervisorExitAnimationFinished()
    {
        gameObject.SetActive(false);
        SpawnFirstStudent();
    }

    private void SpawnFirstStudent()
    {
        if (firstStudent != null)
        {
            firstStudent.SetActive(true);
        }
    }
}
