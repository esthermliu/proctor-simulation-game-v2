using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class ClickableID : MonoBehaviour
{
    public Color hoverColor = Color.yellow;      // color when hovered
    public GameObject enlargedID;                // assign enlarged version in Inspector

    private SpriteRenderer sr;
    private Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        //// Initially hide the small ID, but keep collider active
        //sr.enabled = false;

        if (enlargedID != null)
            enlargedID.SetActive(false); // make sure enlarged is hidden at start
    }

    void OnMouseEnter()
    {

        Debug.Log("HOVER!");
        sr.color = hoverColor; // change color when mouse hovers
    }

    void OnMouseExit()
    {
        sr.color = originalColor; // revert when mouse leaves
    }

    void OnMouseDown()
    {
        if (enlargedID != null)
        {
            // hide this small ID
            gameObject.SetActive(false);

            // show the enlarged version
            enlargedID.SetActive(true);
        }
    }
}
