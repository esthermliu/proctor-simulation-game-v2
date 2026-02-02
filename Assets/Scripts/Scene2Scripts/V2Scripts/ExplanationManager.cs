using UnityEngine;
using System.Collections;

public class ExplanationManager : MonoBehaviour
{
    [Header("All explanation bubbles")]
    public GameObject[] explanationBubbles;

    [Header("Timing")]
    [SerializeField] private float delayBetweenBubbles = 1f;

    private int currentIndex = 0;

    public void StartExplanation()
    {
        currentIndex = 0;
        ShowCurrentBubble();
    }

    private void ShowCurrentBubble()
    {
        if (currentIndex >= explanationBubbles.Length)
            return;

        GameObject bubble = explanationBubbles[currentIndex];
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
        GameObject bubble = explanationBubbles[currentIndex];
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
