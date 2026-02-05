using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("Link to Scene 2 Manager")]
    public Scene2Manager scene2Manager;

    [Header("Link to corresponding student")]
    public Student student;

    private Animator studentAnimator;

    void Start()
    {
        // Get the Animator component from the student
        if (student != null)
        {
            studentAnimator = student.GetComponent<Animator>();
        }
    }

    public void OnCorrectOptionClick()
    {
        // Always hide purple question
        student.HideQuestion();

        // Correct: show good response, hide the exam guide, hide the bad response if visible
        student.ShowGoodResponse();
        student.HideGuide();
        student.HideBadResponse();

        // If Game Manager exists, then increment students helped that day
        if (GameManager.Instance != null)
        {
            GameManager.Instance.HelpStudent();
        }

        // End the question event
        scene2Manager.ResetOngoingEvent();

        // Transition from STILL to IDLE animation
        if (studentAnimator != null)
        {
            studentAnimator.SetTrigger("StartIdle");
        }

    }

    public void OnIncorrectOptionClick()
    {
        // Always hide purple question
        student.HideQuestion();

        // Incorrect: show black, keep orange visible
        student.ShowBadResponse();
    }
}
