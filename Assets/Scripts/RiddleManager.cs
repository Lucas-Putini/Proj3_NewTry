using UnityEngine;
using TMPro;

public class RiddleManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform cubeSpawnPoint;
    public TMP_Text riddleText;
    public GameObject vendingZoneRoot;

    private bool riddleCompleted = false;

    void Start()
    {
        vendingZoneRoot.SetActive(true);
        riddleText.text = "Hungry minds, don't delay,\nFind the place with snacks on display.\nGrab the cube and walk this way,\nTo vending machines where students stay.";
        SpawnCube();
    }

    public void OnCorrectZoneEntered()
    {
        if (riddleCompleted) return;

        riddleText.text = "🎉 Great job! You found the vending machine.";
        riddleCompleted = true;
    }

    void SpawnCube()
    {
        Instantiate(cubePrefab, cubeSpawnPoint.position, Quaternion.identity);
    }
}
