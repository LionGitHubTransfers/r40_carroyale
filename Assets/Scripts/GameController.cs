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

    public int CurrentLevelIndex { get; internal set; }

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
        CurrentLevelIndex = PlayerPrefs.GetInt(Constants.TAG_CURRENT_LEVEL, 0);

        ControllerUI.Init();
        LoadLevel(CurrentLevelIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CurrentLevelIndex = 0;
            PlayerPrefs.SetInt(Constants.TAG_CURRENT_LEVEL, CurrentLevelIndex);
            LoadLevel(CurrentLevelIndex);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Continue();
        }
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
        LoadLevel(CurrentLevelIndex);
    }

    public void Continue()
    {
        CurrentLevelIndex++;
        PlayerPrefs.SetInt(Constants.TAG_CURRENT_LEVEL, CurrentLevelIndex);
        LoadLevel(CurrentLevelIndex);
    }

    public void Loos()
    {
        ControllerUI.ShowContainerLoos();
        ControllerLevel.Loos();
    }

    public void Finish(List<string> liders)
    {
        PlayerPrefs.SetInt(Constants.TAG_CURRENT_LEVEL, CurrentLevelIndex +1);
        ControllerUI.LevelCompleted(liders);
    }
}
