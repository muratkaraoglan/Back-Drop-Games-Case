using UnityEngine;
using System.Collections.Generic;

public class MeshValidPointHelper : MonoBehaviour
{
    public GameObject terrainObject;
    public float normalThreshold = 0.9f;
    public float raycastDistance = 1f;

    private Mesh terrainMesh;
    private Vector3[] terrainVertices;
    private Vector3[] terrainNormals;
    private int[] terrainTriangles;

    public List<Vector3> ValidPoints = new List<Vector3>();

    public void FindValidSurfacePoint()
    {
        ValidPoints.Clear();
        terrainMesh = terrainObject.GetComponent<MeshFilter>().sharedMesh;
        terrainVertices = terrainMesh.vertices;
        terrainNormals = terrainMesh.normals;
        terrainTriangles = terrainMesh.triangles;

        for (int i = 0; i < terrainTriangles.Length; i += 3)
        {

            Vector3 v0 = terrainVertices[terrainTriangles[i]];
            Vector3 v1 = terrainVertices[terrainTriangles[i + 1]];
            Vector3 v2 = terrainVertices[terrainTriangles[i + 2]];


            Vector3 normal = (terrainNormals[terrainTriangles[i]] + terrainNormals[terrainTriangles[i + 1]] + terrainNormals[terrainTriangles[i + 2]]) / 3.0f;


            if (Vector3.Dot(normal, Vector3.up) > normalThreshold)
            {
                float a = Random.value;
                float b = Random.value;
                if (a + b > 1)
                {
                    a = 1 - a;
                    b = 1 - b;
                }

                float c = 1 - a - b;
                Vector3 randomPoint = a * v0 + b * v1 + c * v2;
                Vector3 worldPosition = terrainObject.transform.TransformPoint(randomPoint);

                if (!IsObjectAbove(worldPosition))
                {
                    ValidPoints.Add(worldPosition);
                }
            }
        }
    }

    bool IsObjectAbove(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 0.1f, Vector3.up);
        return Physics.Raycast(ray, raycastDistance);
    }

    public void DrawDebugLines()
    {
        foreach (var point in ValidPoints)
        {
            Debug.DrawLine(point, point + Vector3.up * 10, Color.red, 10f);
        }
    }
}
