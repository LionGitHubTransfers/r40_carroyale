using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public float Radius = 500;
    public float H = 100;

    public Transform Circle;
    public ParticleSystem Fire;

    public ParticleSystem.ShapeModule shapeMod;

    private void Start()
    {
        shapeMod = Fire.shape;
    }

    void Update()
    {
        Circle.localScale = new Vector3(Radius, Radius, H);
       // shapeMod.radius = Radius + 5;
       // Fire.shape.scale = 1;
    }
}
