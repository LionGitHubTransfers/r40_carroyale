using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public List<Collider> ChildCollider;
    public List<Rigidbody> ChildRigidbody;
    private void OnTriggerEnter(Collider other)
    {
        return;
        if (other.tag == "Player")
        {
            foreach (Collider cl in ChildCollider)
                cl.enabled = true;

            foreach (Rigidbody rb in ChildRigidbody)
                rb.useGravity = true;
        }
    }
}
