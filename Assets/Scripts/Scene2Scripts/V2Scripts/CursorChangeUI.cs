using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChangeUI : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Texture2D hoverCursor;
    public Texture2D clickCursor;
    public Vector2 hotspot = Vector2.zero;
    CursorMode cursorMode = CursorMode.Auto;

    private bool isPointerDown = false;
    private RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPointerDown)
            Cursor.SetCursor(hoverCursor, hotspot, cursorMode);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isPointerDown)
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        Cursor.SetCursor(clickCursor, hotspot, cursorMode);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        // Reset cursor to hover if still over the element
        if (RectTransformUtility.RectangleContainsScreenPoint(rt, Input.mousePosition, eventData.enterEventCamera))
            Cursor.SetCursor(hoverCursor, hotspot, cursorMode);
        else
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            // If pointer moves outside while dragging, reset cursor
            if (!RectTransformUtility.RectangleContainsScreenPoint(rt, Input.mousePosition, eventData.enterEventCamera))
            {
                Cursor.SetCursor(null, Vector2.zero, cursorMode);
            }
        }
    }

    void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}