using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerDownHandler
{
    [Header("Enlarged Object")]
    public GameObject enlargedObject;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked UI item");
        transform.SetAsLastSibling();
    }
}
