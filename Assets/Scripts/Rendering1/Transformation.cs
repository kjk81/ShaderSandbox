using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transformation : MonoBehaviour
{
    public abstract Matrix4x4 Matrix { get; } // read-only property

    public Vector3 Apply(Vector3 point) {
        return Matrix.MultiplyPoint(point); // multiply transformation matrix against the point for new point
    }
}
