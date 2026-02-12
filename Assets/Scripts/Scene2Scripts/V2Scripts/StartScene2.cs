using UnityEngine;
using System.Collections;

public class StartScene2 : MonoBehaviour
{
    public Scene2Manager scene2Manager;

    [Header("Student animators")]
    public Animator girl1Animator;
    public Animator girl2Animator;
    public Animator guy1Animator;
    public Animator guy2Animator;

    [Header("Settings")]
    public float hideDelay = 0.5f;


    // attach this script to the speech bubble, so we trigger scene 2 after speech bubble
    void Start()
    {
        GetComponent<TypewriterEffect>().OnTypingComplete += StartDay;
    }

    public void StartDay()
    {
        // first, we want to trigger the writing animations for all the characters
        girl1Animator.SetTrigger("StartWriting");
        girl2Animator.SetTrigger("StartWriting");
        guy1Animator.SetTrigger("StartWriting");
        guy2Animator.SetTrigger("StartWriting");

        // next, call EnableStartTime() from scene2Manager so scene can start
        scene2Manager.EnableStartTime();

        // Start delayed hide
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        gameObject.SetActive(false);
    }
}
