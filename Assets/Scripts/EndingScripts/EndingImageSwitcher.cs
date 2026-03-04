using UnityEngine;
using UnityEngine.UI;

public class EndingImageSwitcher : MonoBehaviour
{
    public enum EndingType
    {
        Good,
        Okay,
        Bad
    }

    [Header("Ending Sprites")]
    public Sprite goodEndingSprite;
    public Sprite okayEndingSprite;
    public Sprite badEndingSprite;

    private Image imageComponent;

    void Awake()
    {
        imageComponent = GetComponent<Image>();

        if (imageComponent == null)
        {
            Debug.LogError("EndingImageSwitcher: No Image component found!");
        }
    }

    void Start()
    {
        SetEnding();
    }

    public void SetEnding()
    {
        if (imageComponent == null) return;

        EndingType ending = DetermineEnding();

        switch (ending)
        {
            case EndingType.Good:
                imageComponent.sprite = goodEndingSprite;
                EventLogger.Log(new GameEvent {
                    eventTypeEnum = EventType.ending_determined,
                    description = "Good",
                });
                break;

            case EndingType.Okay:
                imageComponent.sprite = okayEndingSprite;
                EventLogger.Log(new GameEvent {
                    eventTypeEnum = EventType.ending_determined,
                    description = "Okay",
                });
                break;

            case EndingType.Bad:
                imageComponent.sprite = badEndingSprite;
                EventLogger.Log(new GameEvent {
                    eventTypeEnum = EventType.ending_determined,
                    description = "Bad",
                });
                break;
        }
    }

    private EndingType DetermineEnding()
    {
        // Default to bad ending if game manager is null
        if (GameManager.Instance == null)
        {
            return EndingType.Bad;
        }

        // get stats:
        int totalCorrect = GameManager.Instance.state.totalCorrect;
        int totalFlagged = GameManager.Instance.state.totalFlagged;
        int totalReported = GameManager.Instance.state.totalReported;
        int totalHelped = GameManager.Instance.state.totalHelped;


        // Good Ending
        if (totalCorrect >= 14 &&
            totalHelped == 3 &&
            totalFlagged >= 6 &&
            totalReported == 3)
        {
            return EndingType.Good;
        }

        // Okay Ending
        if (totalCorrect >= 11 &&
            totalHelped >= 2 &&
            totalFlagged >= 5 &&
            totalReported >= 2)
        {
            return EndingType.Okay;
        }

        // Bad Ending
        return EndingType.Bad;
    }
}
