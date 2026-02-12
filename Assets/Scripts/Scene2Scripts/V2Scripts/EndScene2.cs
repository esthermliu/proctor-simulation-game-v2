using UnityEngine;
using System.Collections;

public class EndScene2 : MonoBehaviour
{
    [Header("Student animators")]
    public Animator girl1Animator;
    public Animator girl2Animator;
    public Animator guy1Animator;
    public Animator guy2Animator;

    [Header("Settings")]
    public float hideDelay = 1f;

    [Header("End Day Email Notification")]
    public GameObject endDayEmailNotification;


    // attach this script to the speech bubble, so we trigger end day actions
    void Start()
    {
        GetComponent<TypewriterEffect>().OnTypingComplete += EndDay;
    }

    public void EndDay()
    {
        // first, we want to trigger the pencils down animations for all the characters
        girl1Animator.SetTrigger("StopWriting");
        girl2Animator.SetTrigger("StopWriting");
        guy1Animator.SetTrigger("StopWriting");
        guy2Animator.SetTrigger("StopWriting");

        // Start delayed hide
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        gameObject.SetActive(false);

        // Show the new email notification AFTER the pencils down bubble
        endDayEmailNotification.SetActive(true);
    }
}
