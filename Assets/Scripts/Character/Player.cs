using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBehaviour
{
    private DynamicJoystick _joystickControl => GameController.Controller.ControllerUI.JoystickControl;

    public override void UpdateCharacter()
    {
        if (GameController.Controller.ControllerLevel.IsRaceProgress)
            _direction = Vector3.forward * _joystickControl.Vertical + Vector3.right * _joystickControl.Horizontal;
        else
            _direction = Vector3.zero;

        base.UpdateCharacter();
    }

    public override void DestroyCgaracter()
    {
        base.DestroyCgaracter();
    }
}