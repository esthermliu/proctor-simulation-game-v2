using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioSource TitleTheme;
    public AudioSource AdmissionsTheme;
    public AudioSource ExamRoomTheme;
    public AudioSource ReviewTheme;

    private AudioSource currentTheme;

    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BeginTheme(string sceneName)
    {
        Debug.Log("Beginning theme for scene: " + sceneName);
        // If a theme is already playing, fade it out
        if (currentTheme != null)
        {
            StartCoroutine(FadeOut(currentTheme, 1.0f));
        }

        AudioSource nextTheme = null;

        // Choose theme based on scene name
        if (sceneName == "Scene0_Title")
        {
            nextTheme = TitleTheme;
        }
        else if (sceneName.StartsWith("Scene1_"))
        {
            nextTheme = AdmissionsTheme;
        }
        else if (sceneName.StartsWith("Scene2_"))
        {
            nextTheme = ExamRoomTheme;
        }
        else if (sceneName.StartsWith("Scene3_"))
        {
            nextTheme = ReviewTheme;
        }

        if (nextTheme != null)
        {
            currentTheme = nextTheme;
            currentTheme.volume = 0f;
            if (!currentTheme.isPlaying)
                currentTheme.Play();
            StartCoroutine(FadeIn(currentTheme, 1.0f));
        }
    }

    // Fade out audio source over duration seconds
    private System.Collections.IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }

    // Fade in audio source over duration seconds
    private System.Collections.IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float endVolume = 1f;
        float time = 0f;
        audioSource.volume = 0;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(0, endVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = endVolume;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
