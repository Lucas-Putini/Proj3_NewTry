#if UNITY_EDITOR || DEVELOPMENT_BUILD
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class DevAnchorPlacer : MonoBehaviour
{
    public string anchorName = "Room1Anchor";
    public GameObject anchorRoot;

    private OVRSpatialAnchor _anchor;

    async void Start()
    {
        await Task.Delay(1000); // Wait for XR systems to initialize

        if (anchorRoot != null)
        {
            GameObject anchorObj = new GameObject($"Anchor_{anchorName}");
            anchorObj.transform.SetPositionAndRotation(anchorRoot.transform.position, anchorRoot.transform.rotation);
            _anchor = anchorObj.AddComponent<OVRSpatialAnchor>();

            if (_anchor)
            {
                while (!_anchor.Created)
                {
                    await Task.Yield();
                }

                var saveResult = await _anchor.SaveAnchorAsync();
                Debug.Log($"Spatial Anchor '{anchorName}' save result: {saveResult.Success}");
            }
        }
    }
}
#endif
