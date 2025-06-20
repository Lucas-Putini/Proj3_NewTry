using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RoomAnchorManager : MonoBehaviour
{
    [Tooltip("The GameObject to turn on when our anchor starts tracking.")]
    public GameObject contentToActivate;

    private ARAnchor anchor;
    private ARAnchorManager anchorManager;

    void Awake()
    {
        // grab the ARAnchorManager in the scene
        anchorManager = FindFirstObjectByType<ARAnchorManager>();
        if (anchorManager == null)
        {
            Debug.LogError("ARAnchorManager not found in the scene.");
            return;
        }

        // find the ARAnchor we've already placed
        anchor = GetComponentInChildren<ARAnchor>();
        if (anchor == null)
        {
            Debug.LogError($"ARAnchor not found on {gameObject.name}");
            return;
        }

        // hide the content until we get a trackable event
        contentToActivate.SetActive(false);
    }

    void OnEnable()
    {
        if (anchorManager != null)
            anchorManager.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    void OnDisable()
    {
        if (anchorManager != null)
            anchorManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
    }

    private void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARAnchor> args)
    {
        // look through only the updated anchors
        foreach (var updated in args.updated)
        {
            if (updated == anchor && updated.trackingState == TrackingState.Tracking)
            {
                HandleAnchorNowTracking();
                break;
            }
        }
    }

    private void HandleAnchorNowTracking()
    {
        Debug.Log($"{gameObject.name} anchor is now tracking enabling content.");
        contentToActivate.SetActive(true);

        // unsubscribe so we don't fire again
        anchorManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
    }
}
