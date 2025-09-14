using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformation : Transformation
{

    public float focalLength = 1f;

    // Que es focalLength?
    // focal length makes the position of closer vertices in object-space bigger on the screen
    // and vice versa
    // basically controls fov
    // longer focal length results in objects looking bigger (zooming in), vice versa for 'zooming out'
    public override Matrix4x4 Matrix {
        get {
            Matrix4x4 mat = new Matrix4x4();
            mat.SetRow(0, new Vector4(focalLength, 0f, 0f, 0f));
            mat.SetRow(1, new Vector4(0f, focalLength, 0f, 0f));
            mat.SetRow(2, new Vector4(0f, 0f, 0f, 0f));
            mat.SetRow(3, new Vector4(0f, 0f, 1f, 0f));
            return mat;
        }
    }

}
