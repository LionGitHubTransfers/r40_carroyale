using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform _target; // в инспекторе задаём объект движения
    public float moveSpeed = 0.1f; // скорость движения объекта
    public DynamicJoystick JoystickControl;

    void Update()
    {
        float forwardMove = Input.GetAxis("Vertical") * moveSpeed;
        float sideMove = Input.GetAxis("Horizontal") * moveSpeed;

        //var pos = _target.forward * forwardMove +
        //                    _target.right * sideMove;

        var pos = new Vector3(forwardMove, 0, sideMove);

        Vector3 direction = Vector3.forward * JoystickControl.Vertical + Vector3.right * JoystickControl.Horizontal;
        if (direction != Vector3.zero)
        {
            _target.LookAt(_target.position + direction, Vector3.up);
            _target.position += direction;
        }


        //if (Vector3.Distance(TransformPlatform.position, _targetMovePosition) < 0.003f)
        //{
        //    TransformPlatform.LookAt(_targetMovePosition, Vector3.up);
        //}

        //TransformPlatform.position = Vector3.MoveTowards(TransformPlatform.position, _targetMovePosition, GameController.Controller.PlayerSpead * Time.deltaTime);
    }

}
