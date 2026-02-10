using UnityEngine;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    public System.Action OnFadeInComplete;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false; // allow clicks after fade

        // Notify listeners
        OnFadeInComplete?.Invoke();
    }
}
