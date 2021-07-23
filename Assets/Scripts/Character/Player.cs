using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBehaviour
{
    private DynamicJoystick _joystickControl => GameController.Controller.ControllerUI.JoystickControl;


    void Update()
    {
        Vector3 direction = Vector3.forward * _joystickControl.Vertical + Vector3.right * _joystickControl.Horizontal;
        //Debug.Log($"Player {direction}");
        base.Move(direction);
    }
}