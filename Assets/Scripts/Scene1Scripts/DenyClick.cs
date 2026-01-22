using UnityEngine;

public class DenyClick : MonoBehaviour
{
    public ReviewDecisionController controller;

    void OnMouseDown()
    {
        controller.Deny();
    }
}
