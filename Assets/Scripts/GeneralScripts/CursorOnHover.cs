using UnityEngine;
using UnityEngine.EventSystems;

public class CursorOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D pencilCursor;
    public Vector2 hotspot = Vector2.zero; // where the "tip" clicks
    public bool isCompleteHoverZone = false;

    [Header("Disable hover effect?")]
    public bool disableCursorEffect = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // don't change cursor if effect is disabled
        if (disableCursorEffect)
        {
            return;
        }

        Cursor.SetCursor(pencilCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // don't change cursor if effect is disabled
        if (disableCursorEffect)
        {
            return;
        }

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

    public void SetDisableCursorEffect(bool disable)
    {
        disableCursorEffect = disable;

        // if disabled, then change the cursor back to normal (unless we disabled the complete hover zone)
        OnDisable();
    }
}