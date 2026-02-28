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

    [Header("Supervisor top character")]
    public Animator supervisorTopCharacter;

    //[Header("Help Text")]
    //public string helpText;

    public void OnClockIn()
    {
        // this will end the tutorial and start the day
        BeginDay();

        // then, animate supervisor leaving to the right
        gameObject.GetComponent<Animator>().SetTrigger("SupervisorExit");

        // Animate the little top supervisor character leaving to right as well
        supervisorTopCharacter.SetTrigger("SupervisorExit");

        // Hide the clock-in button
        clockInButton.SetActive(false);
    }

    public void BeginDay() {

        // show the first character's small items and hide the current small items
        SetActiveIfNotNull(smallReviewFolder, false);
        SetActiveIfNotNull(smallExamGuide, false);

        SetActiveIfNotNull(nextSmallReviewFolder, true);
        SetActiveIfNotNull(nextSmallExamGuide, true);

        // then, advance time forward by 10 minutes
        TimeManager.Instance.AdvanceTime(10);

        if (HelpManager.Instance != null && !HelpManager.Instance.HelpPanelOpen())
        {
            //HelpManager.Instance.SetHelpText(helpText);
            
            HelpManager.Instance.ShowHelpPanel();
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetDailyStats();
        }

        EventLogger.Log(
            new GameEvent
            {
                eventTypeEnum = EventType.day_started,
            }
        );
    }

    private void SetActiveIfNotNull(GameObject obj, bool value)
    {
        if (obj != null)
        {
            obj.SetActive(value);
        }
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
