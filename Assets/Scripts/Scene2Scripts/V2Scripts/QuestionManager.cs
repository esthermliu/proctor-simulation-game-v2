using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("Link to Scene 2 Manager")]
    public Scene2Manager scene2Manager;

    [Header("Link to corresponding student")]
    public Student student;

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
    }

    public void OnIncorrectOptionClick()
    {
        // Always hide purple question
        student.HideQuestion();

        // Incorrect: show black, keep orange visible
        student.ShowBadResponse();
    }
}
