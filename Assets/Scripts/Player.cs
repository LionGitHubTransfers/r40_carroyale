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
    public List<float> LevelSize = new List<float>() { .6f, .7f, .8f, .9f, 1f };

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
        CharController.transform.localScale = Vector3.one * LevelSize[_currentLvlIndex];
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

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == Constants.TAG_OBSTACLE)
        //{
        //    var obstacle = other.GetComponent<Obstacle>();
        //    obstacle.SetDamage(DMG);
        //}

        if (other.tag == Constants.TAG_ITEM)
        {
            TriggerEnterWeapon(other);
        }
    }


    public void TriggerEnterWeapon(Collider other)
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

            if (PutItem(item.IdWeapont))
                item.PickUp();

        }
    }

    public void TriggerStayWeapon(Collider other)
    {
        if (other.tag == Constants.TAG_OBSTACLE)
        {
            var obstacle = other.GetComponent<Obstacle>();
            obstacle.SetDamage(_currentDamage * Time.deltaTime);
        }

        if (other.tag == Constants.TAG_ITEM)
        {
            var item = other.GetComponent<Item>();

            if (item.IdWeapont == _currentWeapon.IdWeapont)
                return;

            if (PutItem(item.IdWeapont))
                item.PickUp();

        }
    }

    public bool PutItem(int idWeapon)
    {
        if (idWeapon == -1)
        {
            if (_currentLvlIndex >= 4)
                return false;

            _currentLvlIndex++;

            CharController.transform.localScale = Vector3.one * LevelSize[_currentLvlIndex];
            return true;
        }

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