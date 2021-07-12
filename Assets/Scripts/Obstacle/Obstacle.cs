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
    public Item SpawnItem;

    public List<FragmentGroup> FragmentGroups;

    private int _countHit = 0;

    public void SetDamage(float dmg)
    {
        if(dmg >= Armor)
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
        SpawnItem.Init();
        Destroy(gameObject);
    }
}
