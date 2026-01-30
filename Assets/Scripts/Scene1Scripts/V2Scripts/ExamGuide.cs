using UnityEngine;

public class ExamGuide : MonoBehaviour
{
    // Initially set currentCourse to the first course in the tabs (counting vertically)
    public GameObject currentCourse;

    public void OnCourseClicked(GameObject clickedCourse)
    {
        Debug.Log("Current course " + currentCourse.name + " and updated course " + clickedCourse.name); ;

        // make the current course inactive
        currentCourse.SetActive(false);

        // make the clicked course active and update the currentCourse
        clickedCourse.SetActive(true);
        currentCourse = clickedCourse;
    }
}
