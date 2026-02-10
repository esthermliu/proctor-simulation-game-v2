using UnityEngine;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public SceneFadeIn fadeIn;

    [Header("Objects to reveal in order")]
    public GameObject[] revealSequence;

    public float delayBetween = 0.5f;

    void Start()
    {
        fadeIn.OnFadeInComplete += ShowIntro;
    }

    // Only show intro text after fade in
    public void ShowIntro()
    {
        StartCoroutine(RevealCoroutine());
    }

    IEnumerator RevealCoroutine()
    {
        for (int i = 0; i < revealSequence.Length; i++)
        {
            revealSequence[i].SetActive(true);
            yield return new WaitForSeconds(delayBetween);
        }
    }
}
