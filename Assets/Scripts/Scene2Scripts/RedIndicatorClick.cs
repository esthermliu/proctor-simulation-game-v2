using UnityEngine;

public class RedIndicatorClick : MonoBehaviour
{
    private Student student;

    void Start()
    {
        student = GetComponentInParent<Student>();
    }

    private void OnMouseDown()
    {
        // Hide red immediately
        student.HideIndicator();

        // Show yellow consequence
        student.ShowInvestigate();
    }
}
