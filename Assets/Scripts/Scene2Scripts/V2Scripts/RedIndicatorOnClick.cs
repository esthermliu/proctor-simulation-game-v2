using UnityEngine;
using UnityEngine.EventSystems;

public class RedIndicatorOnClick : MonoBehaviour, IPointerClickHandler
{
    private Student student;

    void Start()
    {
        student = GetComponentInParent<Student>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Hide red indicator
        student.HideIndicator();

        // Show yellow consequence
        student.ShowInvestigate();
    }
}
