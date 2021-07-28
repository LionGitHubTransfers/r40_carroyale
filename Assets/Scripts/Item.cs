using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Transform ItemTransform;
    public Collider ItemCollider;

    public int IdWeapont;
    public float WeaponDamage;

    public void Init()
    {
        gameObject.SetActive(true);
      //  ItemTransform.parent = GameController.Controller.PatentItems;
        StartCoroutine(SpawnEffect());
    }

    private IEnumerator SpawnEffect()
    {
        yield return new WaitForSeconds(1f);
        ItemCollider.enabled = true;
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
