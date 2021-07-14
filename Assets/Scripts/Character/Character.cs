using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody Rb;
    public float moveSpeed = 0.1f; // скорость движения объекта
    public DynamicJoystick JoystickControl;

    public Transform CamPosition;

    void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * JoystickControl.Vertical + Vector3.right * JoystickControl.Horizontal;

        if (direction != Vector3.zero)
        {
            //Debug.Log($"JoystickControl.Vertical {JoystickControl.Vertical} JoystickControl.Horizontal {JoystickControl.Horizontal}" );
            //Rb.AddForce(direction * moveSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
            //Rb.velocity =direction * moveSpeed * Time.fixedDeltaTime;
            Rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
            // transform.Translate(direction);
            CamPosition.position = Rb.position;
        }

    }
}
