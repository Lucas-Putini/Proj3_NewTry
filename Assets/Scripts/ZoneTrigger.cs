using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            FindObjectOfType<RiddleManager>().OnCorrectZoneEntered();
        }
    }
}
