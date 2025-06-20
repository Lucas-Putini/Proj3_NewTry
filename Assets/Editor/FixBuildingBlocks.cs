using UnityEngine;
using UnityEditor;

public class FixBuildingBlocks : EditorWindow
{
    [MenuItem("Tools/Fix Building Blocks")]
    public static void FixBlocks()
    {
        EditorPrefs.DeleteKey("Meta/BuildingBlocks/Blocks");
        Debug.Log("Building Blocks cache cleared. Restart Unity to see Building Blocks.");
    }
} 