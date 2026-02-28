using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEnlarge : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public float hoverScale = 1.1f;

    private Vector3 originalScale;
    private bool clickable = true;

    void Awake()
    {
        originalScale = transform.localScale;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!clickable) return;
        transform.localScale = originalScale;
    }

    public void SetClickable(bool clickable)
    {
        this.clickable = clickable;
    }
}
