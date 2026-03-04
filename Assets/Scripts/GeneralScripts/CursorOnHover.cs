using UnityEngine;
using UnityEngine.EventSystems;

public class CursorOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D pencilCursor;
    public Vector2 hotspot = Vector2.zero; // where the "tip" clicks
    public bool isCompleteHoverZone = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(pencilCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void OnDisable()
    {
        // don't change cursor when complete hover zone is disabled
        if (isCompleteHoverZone)
        {
            return;
        }

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}