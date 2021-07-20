using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Controller;

    public UiController ControllerUI;

    public Transform ParentItems;
    public Transform PatentFragment;

    public Item SpawnItemArmor;
    public List<Item> ListSpawnItemsWeapon;

    public Transform PatentItemsl { get; internal set; }

    private int _countSpawnArmor = 0;
    private int _countSpawnWeapon = 0;

    private void Awake()
    {
        if (Controller == null)
        {
            Controller = this;
            return;
        }

        Destroy(this);
    }

    public Item GetItem()
    {
        if(_countSpawnArmor <= _countSpawnWeapon)
        {
            _countSpawnArmor++;
            return SpawnItemArmor;
        }
        else
        {
            _countSpawnWeapon++;
            return ListSpawnItemsWeapon[ListSpawnItemsWeapon.Count - 1];
        }
    }
}
