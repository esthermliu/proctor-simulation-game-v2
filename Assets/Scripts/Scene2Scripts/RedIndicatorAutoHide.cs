using UnityEngine;

public class RedIndicatorAutoHide : MonoBehaviour
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
