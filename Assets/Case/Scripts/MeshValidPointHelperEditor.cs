using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshValidPointHelper))]
public class MeshValidPointHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MeshValidPointHelper pointFinder = (MeshValidPointHelper)target;

        if (GUILayout.Button("Find Valid Surface Points"))
        {
            pointFinder.FindValidSurfacePoint();
            Debug.Log("Found " + pointFinder.ValidPoints.Count + " valid points.");
        }

        if (GUILayout.Button("Draw Points"))
        {
            pointFinder.DrawDebugLines();
        }
    }
}
