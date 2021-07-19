using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject WeaponGO;
    public float Damage = 1;
    public int IdWeapont;

    public bool IsCumulativeDamage = false;

    private Player _characher;

    public void Init(Player pl)
    {
        _characher = pl;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCumulativeDamage)
            return;

        _characher.TriggerEnterWeapon(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsCumulativeDamage)
            return;

        _characher.TriggerStayWeapon(other);
    }

    public void SetActive(bool flag)
    {
        WeaponGO.SetActive(flag);
    }
}
