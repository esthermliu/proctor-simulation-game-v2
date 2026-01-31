using UnityEngine;
using UnityEngine.EventSystems;

public class GreenIndicatorOnClick : MonoBehaviour, IPointerClickHandler
{
    private Student student;

    void Start()
    {
        student = GetComponentInParent<Student>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Hide green immediately
        student.HideIndicator();

        // Show purple + orange
        student.ShowQuestion();
        student.ShowGuide();
    }
}
