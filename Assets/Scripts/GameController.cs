using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Controller;

    public UiController ControllerUI;
    public LevelController ControllerLevel;
    public Config Config;

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
    private void Start()
    {
        ControllerUI.Init();
        LoadLevel(0);
       // ControllerLevel.LoadLevel(0);
    }

    public Item GetItem()
    {
        if(_countSpawnArmor <= _countSpawnWeapon)
        {
            _countSpawnArmor++;
            return Config.SpawnItemArmor;
        }
        else
        {
            _countSpawnWeapon++;
            return Config.ListSpawnItemsWeapon[Random.Range(0, Config.ListSpawnItemsWeapon.Count)];
        }
    }

    public void LoadLevel(int indexLevel)
    {
        ControllerLevel.ClearMap();
        ControllerLevel.LoadLevel(indexLevel);
    }

    public void Restart()
    {
        LoadLevel(0);
    }

    public void Continue()
    {
        LoadLevel(0);
    }

    public void Loos()
    {
        ControllerUI.ShowContainerLoos();
        ControllerLevel.Loos();
    }

    public void Finish(List<string> liders)
    {
        ControllerUI.LevelCompleted(liders);
    }
}
