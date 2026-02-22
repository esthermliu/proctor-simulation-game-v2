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

    public GameObject helpComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        helpComponent.SetActive(false);
    }

    public void ShowHelpPanel()
    {
        helpComponent.SetActive(true);
    }

    public void HideHelpPanel()
    {
        helpComponent.SetActive(false);
    }

    public void SetHelpText(string text)
    {
        helpComponent.GetComponentsInChildren<UnityEngine.UI.Image>(true)[1].GetComponentsInChildren<TMPro.TextMeshProUGUI>(true)[1].text = text;
    }

    public void LogHelpClicked() 
    {
        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.help_clicked,
        });
    }
}
