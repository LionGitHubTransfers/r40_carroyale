using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBehaviour
{
    private DynamicJoystick _joystickControl => GameController.Controller.ControllerUI.JoystickControl;

    public override void UpdateCharacter()
    {
        _direction = Vector3.forward * _joystickControl.Vertical + Vector3.right * _joystickControl.Horizontal;
        base.UpdateCharacter();
    }
}