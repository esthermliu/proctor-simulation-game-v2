using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadePanel; // Assign your black panel here
    public float fadeDuration = 1f; // Duration of fade in/out

    private void Start()
    {
        // Start with black panel covering the screen
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        Color color = fadePanel.color;
        color.a = 1f; // start fully opaque
        fadePanel.color = color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration); // fade to transparent
            fadePanel.color = color;
            yield return null;
        }

        color.a = 0f;
        fadePanel.color = color; // ensure fully transparent
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float timer = 0f;
        Color color = fadePanel.color;
        color.a = 0f; // start transparent
        fadePanel.color = color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration); // fade to black
            fadePanel.color = color;
            yield return null;
        }

        color.a = 1f;
        fadePanel.color = color; // ensure fully opaque

        SceneManager.LoadScene(sceneName);
    }
}
