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

    [Header("Auto Start")]
    [SerializeField] private bool autoStartOnEnable = false;

    public event Action OnTypingComplete;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        // Safety: auto-grab TMP if not assigned
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();
    }


    // Auto-start text on enable (for some objects)
    private void OnEnable()
    {
        if (autoStartOnEnable)
        {
            StartTyping(textMessage);
        }
    }

    // Call this from a button, click, or event
    public void StartTyping(string message)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    // Overloading function that types message in public field
    public void StartTyping()
    {
        StartTyping(textMessage);
    }

    private IEnumerator TypeText(string message)
    {
        textComponent.text = message;
        textComponent.maxVisibleCharacters = 0;

        int totalChars = message.Length;

        for (int i = 0; i <= totalChars; i++)
        {
            textComponent.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typeSpeed);
        }

        typingCoroutine = null;
        OnTypingComplete?.Invoke();
    }
}
