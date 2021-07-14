using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public float Radius = 500;
    public float Height = 20;

    public Transform �ransformRing;

    void Update()
    {
        �ransformRing.localScale = new Vector3(Radius, Radius, Height);
    }
}
