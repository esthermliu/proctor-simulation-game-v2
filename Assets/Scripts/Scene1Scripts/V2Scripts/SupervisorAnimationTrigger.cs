using UnityEngine;

public class SupervisorAnimationTrigger : MonoBehaviour
{
    public GameObject supervisor;

    public void TriggerSupervisorEntrance() {
        supervisor.SetActive(true);
    }
}
