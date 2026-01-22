using UnityEngine;
using System.Collections;

public class ClickToDisappear : MonoBehaviour
{
    void Start()
    {
        Debug.Log("TESTING");
    }

    void OnMouseDown()
    {
        // This hides the whole GameObject
        Debug.Log("HIDE");
        gameObject.SetActive(false);

        // OR, to just hide the sprite but keep the object active:
        // GetComponent<SpriteRenderer>().enabled = false;
    }
}
