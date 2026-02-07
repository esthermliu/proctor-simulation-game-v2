using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("All dialogue bubbles")]
    public GameObject[] dialogueBubbles;

    [Header("Timing")]
    [SerializeField] private float delayBetweenBubbles = 0.5f;

    [Header("Performance Eval")]
    public PerformanceEvaluation performanceEvaluation;

    [Header("Supervisor Animator")]
    public Animator supervisorAnimator;

    private int currentIndex = 0;

    // This function should be called after the player clicks on the supervisor talk bubble
    public void StartDialogue()
    {
        currentIndex = 0;
        ShowCurrentBubble();
    }

    private void ShowCurrentBubble()
    {
        if (currentIndex >= dialogueBubbles.Length)
        {
            // No more bubbles (dialogue is complete)
            // We now want to show the performance eval form
            performanceEvaluation.ShowPerformanceEvaluation();

            // We want to trigger typing animation for supervisor again (StartTyping)
            supervisorAnimator.SetTrigger("StartTyping");

            return;
        }

        GameObject bubble = dialogueBubbles[currentIndex];
        bubble.SetActive(true);

        TypewriterEffect typewriter = bubble.GetComponent<TypewriterEffect>();

        if (typewriter != null)
        {
            typewriter.OnTypingComplete += HandleTypingComplete;
            typewriter.StartTyping();
        }
    }

    private void HandleTypingComplete()
    {
        StartCoroutine(AdvanceAfterDelay());
    }

    private IEnumerator AdvanceAfterDelay()
    {
        GameObject bubble = dialogueBubbles[currentIndex];
        TypewriterEffect typewriter = bubble.GetComponent<TypewriterEffect>();

        // Clean up event subscription
        typewriter.OnTypingComplete -= HandleTypingComplete;

        // Optional: leave bubble visible briefly
        yield return new WaitForSeconds(delayBetweenBubbles);

        // Hide current bubble
        bubble.SetActive(false);

        // Advance
        currentIndex++;
        ShowCurrentBubble();
    }
}
