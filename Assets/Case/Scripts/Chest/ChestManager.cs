using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{

    [Header("Chest Initialize")]
    [SerializeField] private Chest _chestPrefab;
    [SerializeField, Range(1, 20)] private int _numberOfChest;
    [SerializeField, Min(0)] private int _maxItemCountInChest = 5;

    [Header("Valid Points")]
    [SerializeField] private List<MeshFilter> _chestSpawnFieldMeshFilters;
    [SerializeField] private float normalThreshold = 0.9f;
    [SerializeField] private float raycastDistance = 100f;
    [SerializeField] private List<Vector3> _validPoints;

    private Mesh terrainMesh;
    private Vector3[] terrainVertices;
    private Vector3[] terrainNormals;
    private int[] terrainTriangles;

    private void Start()
    {
        _validPoints.Shuffle();
        for (int i = 0; i < _numberOfChest; i++)
        {
            Chest chest = Instantiate(_chestPrefab, _validPoints[i], Quaternion.identity);
            chest.SetItems(ItemManager.Instance.CreateRandomNewItems(Random.Range(1, _maxItemCountInChest)));
        }
    }

    public void FindValidSurfacePoints()
    {
        _validPoints = new List<Vector3>();

        for (int j = 0; j < _chestSpawnFieldMeshFilters.Count; j++)
        {
            terrainMesh = _chestSpawnFieldMeshFilters[j].sharedMesh;
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
                    Vector3 worldPosition = _chestSpawnFieldMeshFilters[j].transform.TransformPoint(randomPoint);

                    if (!IsObjectAbove(worldPosition))
                    {
                        ValidPoints.Add(worldPosition);
                    }
                }
            }


        }
    }


    public List<Vector3> ValidPoints => _validPoints;
    bool IsObjectAbove(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 0.1f, Vector3.up);
        return Physics.Raycast(ray, raycastDistance);
    }

    public void DrawDebugLines()
    {
        foreach (var point in _validPoints)
        {
            Debug.DrawLine(point, point + Vector3.up * 10, Color.red, 10f);
        }
    }

}
