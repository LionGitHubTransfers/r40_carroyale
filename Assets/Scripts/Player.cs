using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform TransformMove; // в инспекторе задаём объект движения
    public Transform TransformRotate; // в инспекторе задаём объект движения
    public float moveSpeed = 0.1f; // скорость движения объекта
    public DynamicJoystick JoystickControl;
    public Rigidbody rb;
    public CharacterController CharController;

    public float DMG = 10;

    public Weapon TempItem;
    public Weapon CurrentWeapon;

    private void Start()
    {
        CurrentWeapon.Init(this);
    }

    void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * JoystickControl.Vertical + Vector3.right * JoystickControl.Horizontal;
        if (direction != Vector3.zero)
        {
            CharController.Move(direction * moveSpeed * Time.fixedDeltaTime); 
            TransformRotate.LookAt(TransformRotate.position + direction, Vector3.up);
        }

        if(TransformMove.position.y > 0.08f)
        {
            var tmp = TransformMove.position;
            tmp.y = 0;
            TransformMove.position = tmp;
        }
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

    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.transform.tag == Constants.TAG_OBSTACLE)
    //    {
    //        var obstacle = collision.transform.GetComponent<Obstacle>();
    //        obstacle.SetDamage(DMG);
    //    }

    //    if (collision.transform.tag == Constants.TAG_ITEM)
    //    {
    //        var item = collision.transform.GetComponent<Item>();
    //        TempItem.SetActive(true);
    //        item.PickUp();
    //    }
    //}

    public void TriggerWeapon(Collider other)
    {
        if (other.tag == Constants.TAG_OBSTACLE)
        {
            var obstacle = other.GetComponent<Obstacle>();
            obstacle.SetDamage(DMG);
        }

        if (other.tag == Constants.TAG_ITEM)
        {
            var item = other.GetComponent<Item>();
            TempItem.gameObject.SetActive(true);
            TempItem.Init(this);

            if (CurrentWeapon.gameObject.activeSelf)
                CurrentWeapon.gameObject.SetActive(false);
            else
                TempItem.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            item.PickUp();
            DMG += 20;
        }
    }

}