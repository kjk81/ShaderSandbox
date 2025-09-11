using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotationTransformation : Transformation
{
    public Vector3 rotation;

    public override Vector3 Apply(Vector3 point)
    {
        // index 0 = x, index 1 = y, index 2 = z
        float[] rads = {
            rotation.x * Mathf.Deg2Rad,
            rotation.y * Mathf.Deg2Rad,
            rotation.z * Mathf.Deg2Rad
        };

        // get cos and sin of input radians
        // utilizing array for simplicity
        float[] cos = new float[rads.Length];
        float[] sin = new float[rads.Length];
        for (int i = 0; i < rads.Length; i++)
        {
            cos[i] = Mathf.Cos(rads[i]);
            sin[i] = Mathf.Sin(rads[i]);
        }

        // define axes of the rotation transformation matrix (3x3)
        // Is equal to the three independent rotation matrices multipled together
        Vector3 xAxis = new Vector3(
                cos[1] * cos[2],
                cos[0] * sin[2] + sin[0] * sin[1] * cos[2],
                sin[0] * sin[2] - cos[0] * sin[1] * cos[2] 
            );
        Vector3 yAxis = new Vector3(
                -cos[1] * sin[2],
                cos[0] * cos[2] - sin[0] * sin[1] * sin[2],
                sin[0] * cos[2] + cos[0] * sin[1] * sin[2]
            );
        Vector3 zAxis = new Vector3(
                sin[1],
                -sin[0] * cos[1],
                cos[0] * cos[1]
            );

        // multiply against input point to get new point
        return xAxis * point.x + yAxis * point.y + zAxis * point.z;
        
    }
}
