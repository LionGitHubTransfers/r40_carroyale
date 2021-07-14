using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform TransformRotate;
    public CharacterController CharController;

    [Header("Parameters")]
    public float MoveSpeed = 10f;
    public float DMG = 10;

    public List<float> LevelDamage = new List<float>() { 10, 20, 30, 40, 50 };

    [Header("Weapons")]
    public Weapon[] WeaponsList;

    private Weapon _currentWeapon;
    private int _currentLvlIndex = 0;
    public float _currentDamage => LevelDamage[_currentLvlIndex] + _currentWeapon.Damage;

    private DynamicJoystick _joystickControl => GameController.Controller.ControllerUI.JoystickControl;

    private void Start()
    {
        for (int i = 0; i < WeaponsList.Length; i++)
        {
            WeaponsList[i].Init(this);
            WeaponsList[i].SetActive(false);
        }

        _currentWeapon = WeaponsList[0];
        _currentWeapon.SetActive(true);
    }

    void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * _joystickControl.Vertical + Vector3.right * _joystickControl.Horizontal;
        
        if (direction != Vector3.zero)
        {
            TransformRotate.LookAt(TransformRotate.position + direction, Vector3.up);
        }

        if (CharController.isGrounded)
            direction.y = -1f;
        else
            direction.y = -8f;

        CharController.Move(direction * MoveSpeed * Time.fixedDeltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == Constants.TAG_OBSTACLE)
    //    {
    //        var obstacle = other.GetComponent<Obstacle>();
    //        obstacle.SetDamage(DMG);
    //    }

    //    if (other.tag == Constants.TAG_ITEM)
    //    {
    //        var item = other.GetComponent<Item>();
    //        TempItem.SetActive(true);
    //        item.PickUp();
    //    }
    //}


    public void TriggerWeapon(Collider other)
    {
        if (other.tag == Constants.TAG_OBSTACLE)
        {
            var obstacle = other.GetComponent<Obstacle>();
            obstacle.SetDamage(_currentDamage);
        }

        if (other.tag == Constants.TAG_ITEM)
        {
            var item = other.GetComponent<Item>();

            if (item.IdWeapont == _currentWeapon.IdWeapont)
                return;

            if (PutWeapon(item.IdWeapont))
                item.PickUp();

        }
    }

    public bool PutWeapon(int idWeapon)
    {
        _currentWeapon.SetActive(false);
        _currentWeapon = null;

        for(int i =0; i < WeaponsList.Length; i++)
            if(WeaponsList[i].IdWeapont == idWeapon)
            {
                WeaponsList[i].SetActive(true);
                _currentWeapon = WeaponsList[i];
            }

        if(_currentWeapon == null)
        {
            _currentWeapon = WeaponsList[0];
            _currentWeapon.SetActive(true);

            return false;
        }

        return true;
    }

}