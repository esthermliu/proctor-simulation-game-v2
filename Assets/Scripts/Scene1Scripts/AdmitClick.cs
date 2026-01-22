using UnityEngine;

public class AdmitClick : MonoBehaviour
{
    public ReviewDecisionController controller;

    void OnMouseDown()
    {
        controller.Admit();
    }
}
