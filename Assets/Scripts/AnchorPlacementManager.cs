using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
// using Meta.XR.SpatialAnchors;
// using Meta.XR.Core;

/// <summary>
/// This script handles the placement and saving of spatial anchors using controller input.
/// It should be placed on a GameObject in your scene, for example on your XR Origin.
/// </summary>
public class AnchorPlacementManager : MonoBehaviour
{
    [Tooltip("The prefab to instantiate when placing an anchor. This is for visualization.")]
    public GameObject anchorPrefab;

    [Tooltip("A transform (e.g., a controller) whose position and rotation will be used to place the new anchor.")]
    public Transform placementPose;

    [Tooltip("The Input Action that triggers the placement of the anchor.")]
    public InputActionReference placementAction;

    private void OnEnable()
    {
        if (placementAction != null)
        {
            placementAction.action.Enable();
            placementAction.action.performed += PlaceAnchor;
        }
    }

    private void OnDisable()
    {
        if (placementAction != null)
        {
            placementAction.action.performed -= PlaceAnchor;
        }
    }

    /// <summary>
    /// This method is called when the specified input action is performed.
    /// </summary>
    private async void PlaceAnchor(InputAction.CallbackContext context)
    {
        if (placementPose == null)
        {
            Debug.LogError("Placement Pose Transform has not been assigned.");
            return;
        }

        if (anchorPrefab == null)
        {
            Debug.LogError("Anchor Prefab has not been assigned.");
            return;
        }

        // Instantiate a new GameObject to host the anchor component.
        var anchorObject = new GameObject("SpatialAnchor");
        anchorObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

        // Add the OVRSpatialAnchor component to the GameObject.
        var newAnchor = anchorObject.AddComponent<OVRSpatialAnchor>();

        // Wait for the anchor to be created and located.
        while (!newAnchor.Created && !newAnchor.Localized)
        {
            await Task.Yield();
        }

        // Instantiate our prefab and parent it to the anchor
        Instantiate(anchorPrefab, newAnchor.transform);
            
        Debug.Log("Anchor created, now saving.");
        await SaveAnchor(newAnchor);
    }

    /// <summary>
    /// Saves the anchor to the device's storage.
    /// </summary>
    private async Task SaveAnchor(OVRSpatialAnchor anchor)
    {
        var saveResult = await anchor.SaveAnchorAsync();
        if (saveResult.Success)
        {
            Debug.Log($"Successfully saved anchor with UUID: {anchor.Uuid}");
            // Here, you would typically store the UUID to retrieve the anchor in future sessions.
            // For example, you could add it to a list in a separate manager class.
        }
        else
        {
            Debug.LogError($"Failed to save anchor with UUID: {anchor.Uuid}");
        }
    }
} 