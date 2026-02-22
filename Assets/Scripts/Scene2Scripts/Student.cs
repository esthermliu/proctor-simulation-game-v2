using UnityEngine;

public class Student : MonoBehaviour
{
    [Header("Bad Behavior (red) OR Question (green)")]
    public GameObject indicator;

    [Header("Investigation Link")]
    public GameObject investigate;

    [Header("Question Links")]
    public GameObject question;
    public GameObject guide;
    public GameObject goodResponse;
    public GameObject badResponse;

    [Header("Explanation Manager")]
    public ExplanationManager explanationManager;

    private Animator animator;

    // Keep track of if behavior is missed or not
    private bool behaviorClicked = false;
    private bool questionAnswered = false;

    [Header("Missed Behavior Settings")]
    public float missedNotificationDelay = 2f;
    public GameObject missedBehaviorNotification;

    [Header("Check for Correct Report")]
    public bool shouldReport;
    public GameObject incorrectNotification;

    // keep track of actual report decision
    private bool reported = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // ========== SHOW Methods ==========
    public void ShowIndicator()
    {
        indicator.SetActive(true);

        // trigger the behavior animation
        PlayBehaviorAnimation();
    }

    public void ShowInvestigate()
    {
        investigate.SetActive(true);
    }

    public void ShowExplanation()
    {
        explanationManager.StartExplanation();
    }

    public void ShowQuestion()
    {
        question.SetActive(true);
    }

    public void ShowGuide()
    {
        guide.SetActive(true);
    }

    public void ShowGoodResponse()
    {
        goodResponse.SetActive(true);
    }

    public void ShowBadResponse()
    {
        badResponse.SetActive(true);
    }


    // ========== HIDE Methods ==========
    public void HideIndicator()
    {
        indicator.SetActive(false);
    }

    public void HideInvestigate()
    {
        investigate.SetActive(false);
    }

    public void HideQuestion()
    {
        question.SetActive(false);
    }

    public void HideGuide()
    {
        guide.SetActive(false);
    }

    public void HideGoodResponse()
    {
        goodResponse.SetActive(false);
    }

    public void HideBadResponse()
    {
        badResponse.SetActive(false);
    }


    // Play the animation corresponding to the behavior
    public void PlayBehaviorAnimation()
    {
        animator.SetTrigger("StartBehavior");
    }


    public void BehaviorClicked()
    {
        this.behaviorClicked = true;
    }

    public void CheckBehaviorClicked() {
        if (!this.behaviorClicked)
        {
            StartCoroutine(DelayedMissedNotification());
            EventLogger.Log(new GameEvent {
                eventTypeEnum = EventType.bad_behavior_missed,
                elapsedTime = Scene2Manager.Instance.ElapsedTime,
                description = gameObject.name, // name of the student
            });
        }
    }

    // extra miss check for questions
    public void QuestionAnswered()
    {
        this.questionAnswered = true;
    }

    public void CheckQuestionAnswered() {
        // if there was a question, that question WAS clicked, but it was NOT answered
        if (question != null && this.behaviorClicked && !questionAnswered)
        {
            StartCoroutine(DelayedMissedNotification());
            EventLogger.Log(new GameEvent {
                eventTypeEnum = EventType.question_missed,
                elapsedTime = Scene2Manager.Instance.ElapsedTime,
                description = gameObject.name, // name of the student
            });
        }
    }

    private System.Collections.IEnumerator DelayedMissedNotification()
    {
        yield return new WaitForSeconds(missedNotificationDelay);
        missedBehaviorNotification.SetActive(true);

        // Move to front (top of hierarchy under same parent)
        missedBehaviorNotification.transform.SetAsLastSibling();
    }


    // Mark decision as report
    public void ReportedStudent()
    {
        this.reported = true;
    }

    // Check whether Report Decision was correct
    public void CheckReport()
    {
        bool correct = (this.shouldReport && this.reported) || (!this.shouldReport && !this.reported);
        if (!correct)
        {
            EventLogger.Log(new GameEvent {
                eventTypeEnum = EventType.report_incorrect,
                elapsedTime = Scene2Manager.Instance.ElapsedTime,
                description = gameObject.name, // name of the student
            });
            // show the incorrect notification on delay
            StartCoroutine(DelayedIncorrectNotification());
        } else
        {
            EventLogger.Log(new GameEvent {
                eventTypeEnum = EventType.report_correct,
                elapsedTime = Scene2Manager.Instance.ElapsedTime,
                description = gameObject.name, // name of the student
            });
            // at this point, we know that the report or lack or report was CORRECT
            // if we reported, then increment report student

            // Otherwise, all questions answered, allow for review to be over
            // if YES checkmark for report is true, call GameManager ReportStudent
            // (adding the null check so we can run scene 2 on its own)
            if (GameManager.Instance != null && this.reported)
            {
                GameManager.Instance.ReportStudent();
            }
        }
    }

    private System.Collections.IEnumerator DelayedIncorrectNotification()
    {
        yield return new WaitForSeconds(missedNotificationDelay);
        incorrectNotification.SetActive(true);

        // Move to front (top of hierarchy under same parent)
        incorrectNotification.transform.SetAsLastSibling();
    }

}
