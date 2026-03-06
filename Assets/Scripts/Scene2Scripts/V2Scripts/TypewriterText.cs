using System.Collections;
using TMPro;
using UnityEngine;
using System;

public class TypewriterEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textComponent;
    public string textMessage = "";

    [Header("Settings")]
    [SerializeField] private float typeSpeed = 0.04f;

    [Header("Audio (Optional)")]
    [SerializeField] private AudioSource audioSource;

    [Header("Auto Start")]
    [SerializeField] private bool autoStartOnEnable = false;

    public event Action OnTypingComplete;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (autoStartOnEnable)
        {
            StartTyping(textMessage);
        }
    }

    public void StartTyping(string message)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    public void StartTyping()
    {
        StartTyping(textMessage);
    }

    private IEnumerator TypeText(string message)
    {
        textComponent.text = message;
        textComponent.maxVisibleCharacters = 0;

        int totalChars = message.Length;

        // START AUDIO if assigned
        if (audioSource != null)
        {
            audioSource.Play();
        }

        for (int i = 0; i <= totalChars; i++)
        {
            textComponent.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typeSpeed);
        }

        // STOP AUDIO when typing is done
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        typingCoroutine = null;
        OnTypingComplete?.Invoke();
    }

    public void StopTyping(bool clearText = false)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        if (clearText)
            textComponent.maxVisibleCharacters = 0;

        OnTypingComplete = null;
    }
}