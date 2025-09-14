using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationGrid : MonoBehaviour
{
    public Transform prefab;

    List<Transformation> transformations;
    Matrix4x4 transformation;

    public int gridResolution = 10;

    Transform[] grid; // 3d array of transforms (positions, rotations, and scales)

    void Awake()
    {
        transformations = new List<Transformation>();

        grid = new Transform[gridResolution * gridResolution * gridResolution]; // 10 x 10 x 10 if gridResolution = 10
        for (int i = 0, z = 0; z < gridResolution; z++) {
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++)
                {
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }
    }

    private void Update()
    {
        UpdateTransformation();
        for (int i = 0, x = 0; x < gridResolution; x++) {
            for (int y = 0; y < gridResolution; y++) {
                for (int z = 0; z < gridResolution; z++, i++)
                {
                    grid[i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
    }

    private Vector3 TransformPoint(int x, int y, int z) {
        Vector3 coordinates = GetCoordinates(x, y, z); // getting the coordinates converted from array indices to pos centered at origin

        return transformation.MultiplyPoint(coordinates);
    }

    private void UpdateTransformation() {
        GetComponents<Transformation>(transformations);
        if (transformations.Count > 0)
        {
            transformation = transformations[0].Matrix;
            for (int i = 1; i < transformations.Count; i++)
            {
                transformation = transformations[i].Matrix * transformation;
            }
        }
    }

    // instantiate Transform (which essentially copies the GameObject or whatever),
    // sets local position w.r.t location in grid array
    // sets color with respect to position and normalizes
    private Transform CreateGridPoint(int x, int y, int z) {
        Transform point = Instantiate<Transform>(prefab);
        point.localPosition = GetCoordinates(x, y, z);
        point.GetComponent<MeshRenderer>().material.color = new Color(
                (float)x / gridResolution,
                (float)y / gridResolution,
                (float)z / gridResolution
            );
        return point;
    }

    // domains of x, y, and z currently go from 0 to gridResolution
    // normalize so it's local and centered at origin (0)
    private Vector3 GetCoordinates(int x, int y, int z) 
    {
        return new Vector3(
                x - (gridResolution - 1) * 0.5f,
                y - (gridResolution - 1) * 0.5f,
                z - (gridResolution - 1) * 0.5f
            );
    }
}
