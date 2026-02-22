using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clickable : MonoBehaviour, IPointerDownHandler, 
IPointerEnterHandler, IPointerExitHandler
{
    public static readonly Color DEFAULT_HOVER_COLOR = new Color(0.7f, 0.7f, 0.7f, 1f);
    public GameObject enlargedPaper;
    public Color hoverColor = DEFAULT_HOVER_COLOR;

    public bool logClick = true;

    protected bool clickable = true;

    private Image image; 

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
        if (!clickable) return;
        // 1) Hide the small version of the paper, reset color
        gameObject.SetActive(false);
        if (image != null)
        {
            image.color = Color.white; // reset to original color
        }


        // 2) Show the enlarged version of the paper IF it exists
        if (enlargedPaper != null)
        {
            enlargedPaper.SetActive(true);

            // Bring enlarged paper to the FRONT of all other papers
            enlargedPaper.transform.SetAsLastSibling();
        } else
        {
            Debug.Log("ERROR (Clickable.cs): Missing enlarged version of paper");
        }

        // log if needed
        if (logClick)
        {
            EventLogger.Log(new GameEvent
            {
                eventTypeEnum = EventType.item_clicked,
                sessionId = GameManager.Instance.sessionId,
                subversion = GameManager.Instance.subversion,
                sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
                description = gameObject.name
            });
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!clickable) return;
        if (image != null)
        {
            image.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!clickable) return;
        if (image != null)
        {
            image.color = Color.white; // reset to original color
        }
    }

    public void SetClickable(bool clickable)
    {
        Debug.Log("Setting clickable to " + clickable + " for " + gameObject.name);
        this.clickable = clickable;
        if (image != null)
        {
            if (!clickable)
            {
                image.color = new Color(1f, 1f, 1f, 0.5f); // make it look disabled
            }
            else
            {
                image.color = Color.white; // reset to original color
            }
        }
    }
}
