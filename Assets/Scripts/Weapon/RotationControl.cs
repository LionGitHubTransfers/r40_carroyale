using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationControl : MonoBehaviour
{
    public enum AxisRotation {x, y, z};

    public Transform TargetRotation;
    public AxisRotation Axis;

    public float SpeedRotation = 10;

    void FixedUpdate()
    {
        if(Axis == AxisRotation.x)
            TargetRotation.Rotate(SpeedRotation * Time.fixedDeltaTime, 0,0);

        if (Axis == AxisRotation.y)
            TargetRotation.Rotate(0, SpeedRotation * Time.fixedDeltaTime, 0);

        if (Axis == AxisRotation.z)
            TargetRotation.Rotate(0, 0, SpeedRotation * Time.fixedDeltaTime);
    }
}
