using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public string zoneID; // "Vending" or "Study"
    public RiddleManager riddleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RiddleToken"))
        {
            riddleManager.OnZoneEntered(zoneID);
        }
    }
}
