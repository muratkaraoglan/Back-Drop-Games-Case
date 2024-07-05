using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChestManager))]
public class ChestManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChestManager pointFinder = (ChestManager)target;

        if (GUILayout.Button("Find Valid Surface Points"))
        {
            pointFinder.FindValidSurfacePoints();
            Debug.Log("Found " + pointFinder.ValidPoints.Count + " valid points.");
        }

        if (GUILayout.Button("Draw Points"))
        {
            pointFinder.DrawDebugLines();
        }
    }
}
