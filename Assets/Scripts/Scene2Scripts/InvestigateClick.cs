using UnityEngine;

public class InvestigateClick : MonoBehaviour
{
    public bool isYes;

    private Investigate decision;

    void Start()
    {
        decision = GetComponentInParent<Investigate>();
    }

    private void OnMouseDown()
    {
        if (isYes)
            decision.OnYesClicked();
        else
            decision.OnNoClicked();
    }
}
