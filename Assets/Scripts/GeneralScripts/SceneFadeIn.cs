using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 0.25f;

    public System.Action OnFadeInComplete;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeInDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false; // allow clicks after fade

        // Notify listeners
        OnFadeInComplete?.Invoke();
    }

    public void FadeToScene(string sceneName)
    {
        
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        yield return FadeOut();
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeOut()
    {
        fadeCanvasGroup.blocksRaycasts = true;

        float t = 0f;
        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeOutDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;
    }
}
