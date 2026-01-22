using UnityEngine;

public class Checkbox : MonoBehaviour
{
    public enum CheckboxType { Admit, Deny }
    public CheckboxType type;

    public Sprite uncheckedSprite;
    public Sprite checkedSprite;

    private SpriteRenderer spriteRenderer;
    private bool isChecked = false;

    // Reference to the other checkbox (to uncheck it when this is checked)
    public Checkbox otherCheckbox;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = uncheckedSprite;
    }

    void OnMouseDown()
    {
        if (!isChecked)
        {
            // Check this box
            Check();

            // Uncheck the other box
            if (otherCheckbox != null)
            {
                otherCheckbox.Uncheck();
            }
        }
        else
        {
            // Uncheck this box if clicked again
            Uncheck();
        }
    }

    public void Check()
    {
        isChecked = true;
        spriteRenderer.sprite = checkedSprite;
    }

    public void Uncheck()
    {
        isChecked = false;
        spriteRenderer.sprite = uncheckedSprite;
    }

    public bool IsChecked()
    {
        return isChecked;
    }
}