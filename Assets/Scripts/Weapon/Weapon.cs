using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject WeaponGO;
    public float Damage = 1;
    public int IdWeapont;

    private Player _characher;

    public void Init(Player pl)
    {
        _characher = pl;
    }

    private void OnTriggerEnter(Collider other)
    {
        _characher.TriggerWeapon(other);
    }

    public void SetActive(bool flag)
    {
        WeaponGO.SetActive(flag);
    }
}
