using UnityEngine;

public class ExamGuideLink : MonoBehaviour
{
    public enum LinkType { CSE, Math, Phil, Chem }
    public LinkType linkType;

    public ExamGuideManager manager;

    void OnMouseDown()
    {
        switch (linkType)
        {
            case LinkType.CSE:
                manager.ShowCSE();
                break;
            case LinkType.Math:
                manager.ShowMath();
                break;
            case LinkType.Phil:
                manager.ShowPhil();
                break;
            case LinkType.Chem:
                manager.ShowChem();
                break;
        }
    }
}
