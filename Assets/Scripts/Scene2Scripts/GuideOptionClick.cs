using UnityEngine;

public class GuideOptionClick : MonoBehaviour
{
    public bool isCorrectOption; // Set in Inspector
    private Student student;

    void Start()
    {
        student = GetComponentInParent<Student>();
    }

    private void OnMouseDown()
    {
        // Always hide purple question
        student.HideQuestion();

        if (isCorrectOption)
        {
            // Correct: show pink, hide orange, hide black if visible
            student.ShowGoodResponse();
            student.HideGuide();
            student.HideBadResponse();
        }
        else
        {
            // Incorrect: show black, keep orange visible
            student.ShowBadResponse();
        }
    }
}
