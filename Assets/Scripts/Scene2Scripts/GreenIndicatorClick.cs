using UnityEngine;

public class GreenIndicatorClick : MonoBehaviour
{
    private Student student;

    void Start()
    {
        student = GetComponentInParent<Student>();
    }

    private void OnMouseDown()
    {
        // Hide green immediately
        student.HideIndicator();

        // Show purple + orange
        student.ShowQuestion();
        student.ShowGuide();
    }
}
