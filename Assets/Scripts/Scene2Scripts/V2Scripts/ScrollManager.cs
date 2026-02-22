using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    [Header("Adjacent scroll components")]
    public GameObject prevScroll;
    public GameObject nextScroll;

    // Attach to each individual scroll component

    public void OnPrevClick()
    {
        prevScroll.SetActive(true);

        // hide the current scroll component
        gameObject.SetActive(false);

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.scroll_clicked,
            description = prevScroll.name,
        });
    }

    public void OnNextClick()
    {
        nextScroll.SetActive(true);

        // hide the current scroll component
        gameObject.SetActive(false);

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.scroll_clicked,
            description = nextScroll.name,
        });
    }
}
