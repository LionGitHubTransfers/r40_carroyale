using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBehaviour
{
    public CharacterController character;

    public float RadiusRing = 30;

    private Vector3 _targetPos;
    private float _time;

    public override void Init()
    {
        var pos = Random.insideUnitCircle * RadiusRing;
        _targetPos.x = pos.x;
        _targetPos.z = pos.y;

        Debug.Log(_targetPos);
        base.Init();
    }

    void Update()
    {


        //Vector3 target = waypoint[currentWaypoint].position;
        //target.y = transform.position.y; // keep waypoint at character's height
        Vector3 moveDirection = _targetPos - transform.position;
        moveDirection.y = Constants.GRAVITY;
        if (moveDirection.magnitude < 1 || _time >= 3)
        {
            _time = 0;
           //transform.position = _targetPos; // force character to waypoint position

            var pos = Random.insideUnitCircle * RadiusRing;
            _targetPos.x = pos.x;
            _targetPos.z = pos.y;
        }
        else
        {
            transform.LookAt(_targetPos);
            character.Move(moveDirection.normalized * _currentSpeed * Time.deltaTime);
        }

        _time += Time.deltaTime;
    }
}