using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    private Transform TransformPoint;

    void Start()
    {
        TransformPoint = transform;
    }

    void Update()
    {
        //TransformPoint.rotation = Quaternion.identity;
    }
}
