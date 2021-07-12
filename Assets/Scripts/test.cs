using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter {transform.name}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OnCollisionEnter {transform.name}");
    }
}
