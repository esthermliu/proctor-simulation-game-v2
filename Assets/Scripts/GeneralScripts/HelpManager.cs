using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public static HelpManager Instance { get; private set; }

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

    public GameObject smallHelpComponent;
    public GameObject largeHelpComponent;

    void Start()
    {
        smallHelpComponent.SetActive(false);
    }

    public void ShowHelpPanel()
    {
        // do NOT show small version if large version is open
        if (largeHelpComponent.activeSelf)
        {
            return;
        }
        smallHelpComponent.SetActive(true);
    }

    public bool HelpPanelOpen()
    {
        return largeHelpComponent.activeSelf;
    }

    public void HideHelpPanel()
    {
        smallHelpComponent.SetActive(false);
    }

    public void LogHelpClicked() 
    {
        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.help_clicked,
        });
    }
}
