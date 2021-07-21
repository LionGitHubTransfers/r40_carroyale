using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public Transform TransformRotate;
    public CharacterController CharController;

    public List<GameObject> LevelSkin;
    public Weapon[] WeaponsList;

    protected Weapon _currentWeapon;
    protected int _currentLvlIndex = 0;
    protected float _currentSpeed = 10f;
    protected float _currentDamage => GameController.Controller.Config.LevelDamage[_currentLvlIndex] + _currentWeapon.Damage;

    protected Vector3 direction;
    //private DynamicJoystick _joystickControl => GameController.Controller.ControllerUI.JoystickControl;

    private void Start()
    {
        for (int i = 0; i < WeaponsList.Length; i++)
        {
            WeaponsList[i].Init(this);
            WeaponsList[i].SetActive(false);
        }

        _currentWeapon = WeaponsList[0];
        CharController.transform.localScale = Vector3.one * GameController.Controller.Config.LevelSize[_currentLvlIndex];
        _currentWeapon.SetActive(true);
        _currentSpeed = GameController.Controller.Config.LevelSpeed[_currentLvlIndex];
    }

    protected virtual void Move(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            TransformRotate.LookAt(TransformRotate.position + direction, Vector3.up);
        }

        if (CharController.isGrounded)
            direction.y = Constants.GRAVITY_SIMPLE;
        else
            direction.y = Constants.GRAVITY;

        CharController.Move(direction * _currentSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.TAG_ITEM)
        {
            TriggerEnterWeapon(other);
        }
    }

    public virtual void TriggerEnterWeapon(Collider other)
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

    public virtual void TriggerStayWeapon(Collider other)
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

    protected bool PutItem(int idItem)
    {
        if (idItem == -1)
        {
            if (_currentLvlIndex >= LevelSkin.Count - 1)
                return false;

            SetArmor();
            return true;
        }

        return SetWeapon(idItem);
    }

    private void SetArmor()
    {
        _currentLvlIndex++;

        _currentSpeed = GameController.Controller.Config.LevelSpeed[_currentLvlIndex];
        LevelSkin[_currentLvlIndex].SetActive(true);

        StopAllCoroutines();
        StartCoroutine(SetSize(Vector3.one * GameController.Controller.Config.LevelSize[_currentLvlIndex]));
    }

    private bool SetWeapon(int idItem)
    {
        for (int i = 0; i < WeaponsList.Length; i++)
            if (WeaponsList[i].IdWeapont == idItem)
            {
                _currentWeapon.SetActive(false);
                _currentWeapon = null;

                _currentWeapon = WeaponsList[i];
                _currentWeapon.SetActive(true);

            }

        if (_currentWeapon == null)
            return false;

        return true;
    }

    private IEnumerator SetSize(Vector3 targetSize)
    {
        float time = 0;

        Vector3 startSize = CharController.transform.localScale;

        while (time < 1)
        {
            time += Time.deltaTime;
            CharController.transform.localScale = Vector3.Lerp(startSize, targetSize, time);

            yield return null;
        }

        CharController.transform.localScale = targetSize;
    }
}
