using UnityEngine;

public class ExamGuide : MonoBehaviour
{
    //public GameObject course1;
    //public GameObject course2;
    //public GameObject course3;
    //public GameObject course4;
    //public GameObject course5;

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

    public void OnCourse2Clicked()
    {
        // TODO:
        // 1) set the game object for the current displayed course to inactive
        //      --> if the current course is the same as the clicked course,
        //          then do nothing (return)

        // 1) Do nothing if the clicked course is already shown
        //if (course2.activeSelf)
        //{
        //    return;
        //}

        // 2) 


        // 2) set the game object for course 2 to active
    }


    // Helper functions
    private void OnCourseClicked()
    {

    }
}
