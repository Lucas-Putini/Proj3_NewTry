using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class RiddleManager : MonoBehaviour
{
    public GameObject riddleCubePrefab;
    public Transform cubeSpawnPoint;
    public TextMeshProUGUI riddleText;

    public GameObject vendingZone;
    public GameObject studyZone;

    private int stage = 0;
    private GameObject currentCube;

    void Start()
    {
        ShowRiddle1();
        SpawnCube();
    }

    public void OnZoneEntered(string zoneID)
    {
        if (stage == 0 && zoneID == "Vending")
        {
            stage = 1;
            ShowRiddle2();
            SpawnCube();
        }
        else if (stage == 1 && zoneID == "Study")
        {
            stage = 2;
            ShowFinalMessage();
            if (currentCube != null)
            {
                Destroy(currentCube);
            }
        }
    }

    void ShowRiddle1()
    {
        riddleText.text = "🔹 Enigma 1 – The Food Spot\nWelcome to HSLU's ground...\nPlace the cube in the vending spot!";
    }

    void ShowRiddle2()
    {
        riddleText.text = "🔹 Enigma 2 – The Study Zone\nGreat job, explorer...\nPlace it where monitors glow!";
    }

    void ShowFinalMessage()
    {
        riddleText.text = "🎉 You did it! Explorer level unlocked!";
    }

    void SpawnCube()
    {
        if (currentCube != null)
        {
            Destroy(currentCube);
        }
        currentCube = Instantiate(riddleCubePrefab, cubeSpawnPoint.position, Quaternion.identity);
    }
}
