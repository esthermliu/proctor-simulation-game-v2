using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialClickable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject enlargedPaper;
    public float hoverScale = 1.1f;

    public string whatAmI; // ID, ticket, or guide

    // keep track of which tutorial day we are on
    [Header("Which Day")]
    public bool day1 = false;
    public bool day2 = false;

    [Header("Audio Source (optional)")]
    public AudioSource audioSource;

    // keeps track of when this item can be clicked, set to false for everything at first
    private bool clickable = false;

    // keeps track of whether we are in pause 2
    private bool pause2 = false;

    // keeps track of whether we are in pause 3
    private bool pause3 = false;

    private Image image;
    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    void Start()
    {
        // extra check in the code to hide enlarged paper
        if (enlargedPaper != null)
        {
            enlargedPaper.SetActive(false);
        }
        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Check if the item can be clicked
        if (!clickable) return;

        // reset to original scale
        transform.localScale = originalScale;

        // 1) Hide the small version of the paper
        gameObject.SetActive(false);

        // If there is an audio source, then play the click sound
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

        // 2) Show the enlarged version of the paper IF it exists
        if (enlargedPaper != null)
        {
            enlargedPaper.SetActive(true);

            // Bring enlarged paper to the FRONT of all other papers
            enlargedPaper.transform.SetAsLastSibling();
        }
        else
        {
            Debug.Log("ERROR (Clickable.cs): Missing enlarged version of paper");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!clickable) return;
        transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!clickable) return;
        transform.localScale = originalScale;
    }

    public void SetClickable(bool clickable)
    {
        this.clickable = clickable;
    }

    public void SetPause2(bool occurring)
    {
        pause2 = occurring;
    }

    public void SetPause3(bool occurring)
    {
        pause3 = occurring;
    }

    public bool IsDay1()
    {
        return day1;
    }

    public bool IsPause2()
    {
        return pause2;
    }

    public bool IsPause3()
    {
        return pause3;
    }
}
