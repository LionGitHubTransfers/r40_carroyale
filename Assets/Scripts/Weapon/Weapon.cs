using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player Characher;

    public void Init(Player pl)
    {
        Characher = pl;
    }

    private void OnTriggerEnter(Collider other)
    {
        Characher.TriggerWeapon(other);
    }
}
