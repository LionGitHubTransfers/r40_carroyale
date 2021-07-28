using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRigidbody : MonoBehaviour
{
    public Rigidbody _rigidbody;

    public float ValueForse = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _rigidbody.velocity = Vector3.up * ValueForse;
    }
}
