using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    private Student student;

    void Start()
    {
        student = GetComponentInParent<Student>();
    }

    public void OnCorrectOptionClick()
    {
        // Always hide purple question
        student.HideQuestion();

        // Correct: show pink, hide orange, hide black if visible
        student.ShowGoodResponse();
        student.HideGuide();
        student.HideBadResponse();
    }

    public void OnIncorrectOptionClick()
    {
        // Always hide purple question
        student.HideQuestion();

        // Incorrect: show black, keep orange visible
        student.ShowBadResponse();
    }
}
