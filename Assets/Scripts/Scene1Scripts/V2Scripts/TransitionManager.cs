using UnityEngine;
using System.Collections;

[System.Serializable]
public class RevealItem
{
    public GameObject obj;
    public float delayAfter = 0.5f; // default delay
}

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    public SceneFadeIn fadeIn;

    [Header("Objects to reveal in order")]
    public RevealItem[] revealSequence;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        fadeIn.OnFadeInComplete += ShowIntro;
    }

    public void ShowIntro()
    {
        StartCoroutine(RevealCoroutine());
    }

    IEnumerator RevealCoroutine()
    {
        for (int i = 0; i < revealSequence.Length; i++)
        {
            revealSequence[i].obj.SetActive(true);
            yield return new WaitForSeconds(revealSequence[i].delayAfter);
        }
    }
}
