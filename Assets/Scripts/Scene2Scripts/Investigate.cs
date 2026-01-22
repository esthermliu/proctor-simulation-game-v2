using UnityEngine;

public class Investigate : MonoBehaviour
{
    private Student student;

    void Start()
    {
        student = GetComponentInParent<Student>();
    }

    public void OnYesClicked()
    {
        student.HideInvestigate();
        student.ShowExplanation();
    }

    public void OnNoClicked()
    {
        student.HideInvestigate();
    }
}
