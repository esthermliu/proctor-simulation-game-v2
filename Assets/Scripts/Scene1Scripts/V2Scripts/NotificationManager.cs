using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [Header("Link incorrect icons for corresponding student")]
    public GameObject[] incorrectIcons;

    [Header("Link incorrect messages for corresponding student")]
    public GameObject[] incorrectMessages;

    // current active incorrect icon + message
    //private GameObject currentActiveIcon;
    //private GameObject currentMessage;

    // index of icon/message to display
    private int studentNumber;

    void Start()
    {
        // we start with student 1 (index 0)
        studentNumber = 0;
    }

    // Call this when player makes a wrong decision
    public void ShowIncorrectIcon()
    {
        //prevStudentNum = studentNumber - 1;
        //if (isValidIndex(prevStudentNum))
        //{
        //    incorrectI
        //}
        //      // Make sure there is only ONE incorrect icon shown at a time
        //// We do this by checking if there is already a currentActiveIcon; set to inactive if so
        //      if (currentActiveIcon != null)
        //      {
        //          currentActiveIcon.SetActive(false);
        //      }



        // All previous icons should be wiped out/reset, so there should only be ONE incorrect
        // icon shown at a time. We shouldn't need to perform any additional checks here that
        // there are any pre-existing incorrect icons.

        Debug.Log("Current incorrect icons student number " + studentNumber);


        // display the incorrect icon for the student
        incorrectIcons[studentNumber].SetActive(true);

        //// update current active icon to that icon and the current message (which may not necessarily be visible yet)
        //currentActiveIcon = incorrectIcons[studentNumber];
        //currentMessage = incorrectMessages[studentNumber];
    }

    // TODO: make the incorrect icon disappear after the next student completes
    //          --> if we got student 1 wrong, then incorrect icon should disappear after student 2 completes

    // This method is called in ReviewFolder.cs to reset all PREVIOUS icons (the email icon + the message)
    // after the player makes the next admit/deny choice
    public void ResetIncorrectIcons()
    {
        // TODO: reset the PREVIOUS incorrect icons
        // if studentNumber - 1 is a valid index, then
        // check activeSelf, set both to false
        int prevStudentNum = studentNumber - 1;
        if (isValidIndex(prevStudentNum))
        {
            incorrectIcons[prevStudentNum].SetActive(false);
            incorrectMessages[prevStudentNum].SetActive(false);
        }

        //if (currentActiveIcon != null)
        //{
        //    currentActiveIcon.SetActive(false);
        //}

        //if (currentMessage != null)
        //{
        //    currentMessage.SetActive(false);
        //}
    }

    // ReviewFolder Admit and Deny functions MUST call increment student
    // each time the user finishes making a decision (so we can keep track of which incorrect
    // icon to show)
    public void IncrementStudent()
    {
        studentNumber++;
    }


    // ========== HELPER FUNCTIONS ==============
    private bool isValidIndex(int studentNum)
    {
        return studentNum >= 0 && studentNum < incorrectIcons.Length;
    }
}
