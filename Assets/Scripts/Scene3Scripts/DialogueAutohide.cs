using UnityEngine;

public class DialogueAutohide : MonoBehaviour
{
    public float visibleTime = 3f;

    private void OnEnable()
    {
        Invoke(nameof(Hide), visibleTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
