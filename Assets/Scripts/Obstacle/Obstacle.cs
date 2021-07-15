using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Serializable]
    public struct FragmentGroup
    {
        public Fragment[] Fragments;
    }

    public float Armor = 0;
    //public Item SpawnItem;
    public Transform PointSpawnItem;

    public List<FragmentGroup> FragmentGroups;

    private int _countHit = 0;

    public void SetDamage(float dmg)
    {
        if(dmg >= Armor && _countHit < FragmentGroups.Count)
        {
            foreach (Fragment fg in FragmentGroups[_countHit].Fragments)
                fg.Init();

            _countHit++;

            if (_countHit >= FragmentGroups.Count)
                DestroyObstacle();
        }
    }

    private void DestroyObstacle()
    {
        var item = Instantiate(GameController.Controller.GetItem(), PointSpawnItem.position, PointSpawnItem.rotation, GameController.Controller.PatentItems);
        item.Init();
        Destroy(gameObject);
    }
}
