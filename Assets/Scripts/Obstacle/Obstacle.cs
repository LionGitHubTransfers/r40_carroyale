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
    public float HealthFragmentGroup = 10;
    //public Item SpawnItem;
    public Transform PointSpawnItem;

    public List<FragmentGroup> FragmentGroups;

    private int _countHit = 0;

    private float _currentDamage = 0;

    public void SetDamage(float dmg)
    {
        _currentDamage += dmg;

        if (dmg >= Armor && _countHit < FragmentGroups.Count && _currentDamage >= HealthFragmentGroup)
        {
            while(_currentDamage >= HealthFragmentGroup)
            {
                foreach (Fragment fg in FragmentGroups[_countHit].Fragments)
                    fg.Init();

                _countHit++;
                _currentDamage -= HealthFragmentGroup;

                if (_countHit >= FragmentGroups.Count)
                {
                    DestroyObstacle();
                    return;
                }
            }
        }
    }

    private void DestroyObstacle()
    {
        var pos = PointSpawnItem.position;
        pos.y = 0;

        var item = Instantiate(GameController.Controller.GetItem(), pos, Quaternion.identity, GameController.Controller.ControllerLevel.ParentItems);
        item.Init();
        Destroy(gameObject);
    }
}
