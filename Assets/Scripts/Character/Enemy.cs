using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBehaviour
{
    public CharacterController character;

    //public float RadiusRing = 30;

    private Vector3 _targetPos;
    private float _time;

    public override void Init(string name)
    {
        NextTargetPosition();

        base.Init(name);
    }

    public override void UpdateCharacter()
    {
        _direction = _targetPos - transform.position;
        if (_direction.magnitude < 1 || _time >= 3)
        {
            _time = 0;

            NextTargetPosition();
        }

        _direction = _direction.normalized;
        _time += Time.deltaTime;

        base.UpdateCharacter();
    }

    private void NextTargetPosition()
    {
        var pos = Random.insideUnitCircle * RadiusRing;
        _targetPos.x = pos.x;
        _targetPos.z = pos.y;
    }
}