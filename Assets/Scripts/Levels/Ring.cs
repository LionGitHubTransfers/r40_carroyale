using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public float Radius = 500;
    public float Height = 20;

    public Transform ÅransformRing;

    void Update()
    {
        ÅransformRing.localScale = new Vector3(Radius, Radius, Height);
    }
}
