using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform _target; // в инспекторе задаём объект движения
    public float moveSpeed = 0.1f; // скорость движения объекта
    public float verticalSpeed = 0.05f; // скорость подъема объекта (Space/Ctrl)

    void Update()
    {
        // здесь блок перемещения объекта WSAD
        float forwardMove = Input.GetAxis("Vertical") * moveSpeed;
        float sideMove = Input.GetAxis("Horizontal") * moveSpeed;
        float verticalMove = Input.GetAxis("Jump") * verticalSpeed;
        _target.position += _target.forward * forwardMove +
                            _target.right * sideMove;
    }
}


/*
 *using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isCheck = true;

    public bool IsMove => _isMove;
    private bool _isMove = true;

    private bool _isAlive = true;

    public Transform TransformPlatform = null;
    public Collider ColliderCharacter = null;

    private CharacterSkin _playerSkin = null;
    private Vector3 _targetMovePosition;
    private int _currentIndexLineRender;
    private bool _isTargetRaised = false;

    public LineRenderer LineRender => _lineRender;
    private LineRenderer _lineRender = null;

    public Vector3 PlayerStartPosition { get; set; }

    private void Start()
    {
        TransformPlatform = transform;
        _isMove = false;
    }

    void Update()
    {
       // if (GameController.Controller.IsMove == false || !_isMove)
        if (!_isMove || !GameController.Controller.IsMove)
            return;

        //if (_isTargetRaised)
        //    if (Vector3.Distance(TransformPlatform.position, PlayerStartPosition) < 0.2f)
        //        GameController.Controller.Finish();

        if (Vector3.Distance(TransformPlatform.position, _targetMovePosition) < 0.003f)
        {
            _currentIndexLineRender++;
            if (_currentIndexLineRender >= _lineRender.positionCount)
            {
               // GameController.Controller.IsMove = false;
                _isMove = false;
                _currentIndexLineRender = 1;
                _playerSkin.StartAnimationWait();
                return;
            }

            _targetMovePosition = GetPosition(_currentIndexLineRender);
            TransformPlatform.LookAt(_targetMovePosition, Vector3.up);
        }

        TransformPlatform.position = Vector3.MoveTowards(TransformPlatform.position, _targetMovePosition, GameController.Controller.PlayerSpead * Time.deltaTime);
    }

    public void Init(CharacterSkin skin, Vector3 pos, LineRenderer lineRender, Level lvl)
    {
        _lineRender = lineRender;

        if (_playerSkin != null)
        {
            Destroy(_playerSkin.gameObject);
            _playerSkin = null;
        }
        _playerSkin = Instantiate(skin, this.transform);
        _playerSkin.transform.localPosition = Vector3.zero;

        _playerSkin.SetMaterials(lvl.MaterialPlayer, lvl.MaterialEnemy);

        PlayerStartPosition = pos;
        TransformPlatform.position = PlayerStartPosition;
    }

    public void SetStartState()
    {
        _isTargetRaised = false;
        _isMove = false;
        _isAlive = true;
        ColliderCharacter.enabled = true;
        TransformPlatform.position = PlayerStartPosition;
        TransformPlatform.rotation = Quaternion.identity;
        _playerSkin.SetDefaultStates();
    }

    public void Stop()
    {
        if (!_isAlive)
            return;

        _isMove = false;
        _playerSkin.StartAnimationWait();
    }

    public void StartMove()
    {
        _isMove = false;

        if (_lineRender.positionCount <= 0)
            return;

        _isMove = true;
        isCheck = true;

        _targetMovePosition = PlayerStartPosition;
        _currentIndexLineRender = 0;
        TransformPlatform.position = GetPosition(0);

        TransformPlatform.LookAt(GetPosition(1), Vector3.up);
        _playerSkin.StartAnimationRun();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!_isAlive)
            return;

        if (collision.tag == Constants.TagFlagSpawn)
        {
            var flagSpawn = collision.GetComponent<FlagSpawn>();

            if (flagSpawn)
            {
                if (flagSpawn.IsPlayerPlatform && _isTargetRaised)
                {
                    flagSpawn.ReturnedFlagEnemy();
                   // isCheck = false;
                    _isTargetRaised = false;
                    _playerSkin.HideTarget();
                }

                if(!flagSpawn.IsPlayerPlatform && flagSpawn.IsActiveFlagEnemy && _isTargetRaised == false)
                {
                    flagSpawn.StealFlagEnemy();
                    //isCheck = false;
                    _isTargetRaised = true;
                    _playerSkin.ShowTarget();
                }

                if (!flagSpawn.IsPlayerPlatform && flagSpawn.IsActiveFlagPlayer)
                {
                    GameController.Controller.FlagSpawnPlayer.ReturnedFlagPlayer();
                    flagSpawn.StealFlagPlayer();
                }
            }
                

            //collision.transform.parent = transform;
            //collision.transform.localPosition = new Vector3(0, 1, 0);
        }

        if(collision.tag == Constants.TagEnemy)
        {
            _isMove = false;
            _isAlive = false;
            //GameController.Controller.Fail();
            _playerSkin.StartAnimationCrash_2();

            if (_isTargetRaised)
            {
                GameController.Controller.FlagSpawnEnemy.ReturnedFlagEnemy();
                _playerSkin.HideTarget();
            }
            ColliderCharacter.enabled = false;
        }

        if (_isMove && collision.tag == Constants.TagCoin)
        {
            var coin = collision.GetComponent<Coin>();
            coin.PickUpCoin();
        }
    }

    private Vector3 GetPosition(int i)
    {
        return _lineRender.GetPosition(i);
    }
}

 */
