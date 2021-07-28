using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public Transform TransformRotate;
    public CharacterController CharController;
    public GameObject WeaponGroups;
    public Transform PointStatusBar;

    public Transform RightWheel_1;
    public Transform RightWheel_2;
    public Transform LeftWheel_1;
    public Transform LeftWheel_2;

    public List<GameObject> LevelSkin;
    public Weapon[] WeaponsList;

    [Serializable]
    public struct FragmentGroup
    {
        public Fragment[] Fragments;
    }

    public List<FragmentGroup> FragmentGroups;

    private GameObject _characterGameObject;
    private Transform _characterTransform;

    protected Weapon _currentWeapon;
    protected int _currentLvlIndex = 0;
    protected float _currentSpeed = 10f;
    protected float _currentDamage => GameController.Controller.Config.LevelDamage[_currentLvlIndex] + _currentWeaponDamage;// _currentWeapon.Damage;
    protected float _currentWeaponDamage = 0;
    protected float RadiusRing => GameController.Controller.ControllerLevel.RadiusRing;

    protected Vector3 _direction;

    private float _currentHealth;

    public bool IsLife => _isLife;
    private bool _isLife;

    private StatusBar Bar;
    public string NameCharacter { get; private set; }


    private void Start()
    {
        // Init();
       //CharController.attachedRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

    public virtual void Init(string name)
    {
        for (int i = 0; i < WeaponsList.Length; i++)
        {
            WeaponsList[i].Init(this);
            WeaponsList[i].SetActive(false);
        }

        _currentWeapon = WeaponsList[0];
        _currentWeaponDamage = GameController.Controller.Config.DefaultWeaponDamage;
        CharController.transform.localScale = Vector3.one * GameController.Controller.Config.LevelSize[_currentLvlIndex];
        _currentWeapon.SetActive(true);
        _currentSpeed = GameController.Controller.Config.LevelSpeed[_currentLvlIndex];
        _currentHealth = GameController.Controller.Config.LevelHealth[_currentLvlIndex];

        _characterGameObject = gameObject;
        _characterTransform = transform;
        _isLife = true;

        Bar = Instantiate(GameController.Controller.Config.StatusBarCharacter, GameController.Controller.ControllerUI.ContainerCharacterStatusBar);
        Bar.Init(PointStatusBar, name, _currentHealth);
        NameCharacter = name;
        _isLife = false;
    }

    public void StartRace()
    {
        _isLife = true;
    }

    public void StopRace()
    {
        _isLife = false;
    }

    private void Update()
    {
        if (_isLife)
        {
            UpdateCharacter();
            CheckInRing();
        }
    }

    public virtual void UpdateCharacter()
    {
        Move(_direction);
    }

    public void CheckInRing()
    {

        float a = Mathf.Pow(_characterTransform.position.x, 2) + Mathf.Pow(_characterTransform.position.z, 2);
        float b = Mathf.Pow(RadiusRing, 2);
       // if (Mathf.Pow(_characterTransform.position.x, 2) + Mathf.Pow(_characterTransform.position.z, 2) >= Mathf.Pow(RadiusRing,2))
        if (Mathf.Pow(_characterTransform.position.x, 2) + Mathf.Pow(_characterTransform.position.z, 2) >= Mathf.Pow(RadiusRing,2))
            SetDamage(GameController.Controller.Config.DamageRing * Time.deltaTime);
        //(x - center_x) ^ 2 + (y - center_y) ^ 2 < radius ^ 2
    }

    protected virtual void Move(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            TransformRotate.LookAt(TransformRotate.position + direction, Vector3.up);

            RightWheel_1.Rotate(0, 0, GameController.Controller.Config.SpeedRotationWheel * Time.deltaTime);
            RightWheel_2.Rotate(0, 0, GameController.Controller.Config.SpeedRotationWheel * Time.deltaTime);
            LeftWheel_1.Rotate(0, 0, GameController.Controller.Config.SpeedRotationWheel * Time.deltaTime);
            LeftWheel_2.Rotate(0, 0, GameController.Controller.Config.SpeedRotationWheel * Time.deltaTime);
        }

        if (CharController.isGrounded)
            direction.y = GameController.Controller.Config.GravitySimple;
        else
            direction.y = GameController.Controller.Config.Gravity;

        CharController.Move(direction * _currentSpeed * Time.deltaTime);
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

            if (PutItem(item))
                item.PickUp();
        }

        if (other.tag == Constants.TAG_CHARACTER)
        {
            if(other.gameObject != _characterGameObject)
            {
                var character = other.GetComponent<CharacterBehaviour>();
                character.SetDamage(_currentDamage);
            }
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

            if (PutItem(item))
                item.PickUp();
        }

        if (other.tag == Constants.TAG_CHARACTER)
        {
            if (other.gameObject != _characterGameObject)
            {
                var character = other.GetComponent<CharacterBehaviour>();
                character.SetDamage(_currentDamage * Time.deltaTime);
            }
        }
    }

    protected bool PutItem(Item item)
    {
        if (item.IdWeapont == -1)
        {
            if (_currentLvlIndex >= LevelSkin.Count - 1)
                return false;

            SetArmor();
            return true;
        }

        return SetWeapon(item);
    }

    private void SetArmor()
    {
        _currentLvlIndex++;

        _currentSpeed = GameController.Controller.Config.LevelSpeed[_currentLvlIndex];
        _currentHealth = GameController.Controller.Config.LevelHealth[_currentLvlIndex];
        LevelSkin[_currentLvlIndex].SetActive(true);

        Bar.SetTextHealth(_currentHealth);

        StopAllCoroutines();
        StartCoroutine(SetSize(Vector3.one * GameController.Controller.Config.LevelSize[_currentLvlIndex]));
    }

    private bool SetWeapon(Item item)
    {
        for (int i = 0; i < WeaponsList.Length; i++)
            if (WeaponsList[i].IdWeapont == item.IdWeapont)
            {
                _currentWeapon.SetActive(false);
                _currentWeapon = null;

                _currentWeapon = WeaponsList[i];
                _currentWeapon.SetActive(true);

                _currentWeaponDamage = item.WeaponDamage;
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

    public void SetDamage(float dmg)
    {
        _currentHealth -= dmg;
        Bar.SetTextHealth(_currentHealth);
        if (_currentHealth <= 0 && _isLife)
        {
            if(NameCharacter == Constants.TAG_PLAYER)
                GameController.Controller.Loos();
            else
                GameController.Controller.ControllerLevel.DeathCharacter(this);

            DestroyCgaracter();
        }
    }

    public virtual void DestroyCgaracter()
    {
        _isLife = false;
        Bar.DestroyStatusBar();

        for (int i = 0; i <= _currentLvlIndex; i++)
            if(i< FragmentGroups.Count)
                foreach (Fragment fg in FragmentGroups[i].Fragments)
                    fg.Init();

        this.enabled = false;
        CharController.enabled = false;
        WeaponGroups.SetActive(false);

    }
}
