using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Ring TaperingRing;

    public Transform ParentItems => _curentMap.ParentItems;
    public Transform PatentFragment => _curentMap.PatentFragment;
    public Transform PatentCharacters => _curentMap.PatentCharacters;

    private Map _curentMap;

    public float RadiusRing { get; private set; }
    public bool IsRaceProgress { get; private set; }

    public List<string> ListNameEnemies { get; private set; } = new List<string>();
    private int _countEnemes;

    private int _currentLevelIndex;

    public void LoadLevel(int levelIndex)
    {
        _currentLevelIndex = levelIndex;
        if (_curentMap != null)
        {
            Destroy(_curentMap.gameObject);
            _curentMap = null;
        }

        _curentMap = Instantiate(GameController.Controller.Config.LevelMaps[0], transform);
        _curentMap.Init(levelIndex);

        RadiusRing = GameController.Controller.Config.RadiusRing.Evaluate(levelIndex);
        TaperingRing.SetRadius(RadiusRing);

        GameController.Controller.ControllerUI.ShowTapToStart();

        _countEnemes = 4;
        ListNameEnemies.Clear();
        IsRaceProgress = true;

        StopAllCoroutines();

        IntegrationManager.Instance.OnLevelStart(levelIndex +1);
    }

    public void StartRace()
    {
        _curentMap.StartRace();
        StartCoroutine(MoveRing());
    }

    public void StopRace()
    {
        _curentMap.StopRace();
    }

    public void DeathCharacter(CharacterBehaviour c)
    {
        ListNameEnemies.Add(c.NameCharacter);

        if (ListNameEnemies.Count >= _countEnemes)
        {
            Finish();
        }
    }

    public void ClearMap()
    {
        if (_curentMap != null)
            _curentMap.DestroyMap();
    }

    private IEnumerator MoveRing()
    {
        yield return new WaitForSeconds(GameController.Controller.Config.DelayRing);

        while(RadiusRing >= GameController.Controller.Config.MinRadiusRing)
        {
            RadiusRing -= Time.deltaTime * GameController.Controller.Config.SpeedRing;
            TaperingRing.SetRadius(RadiusRing);
            yield return null;
        }
    }

    public void Loos()
    {
        IntegrationManager.Instance.OnLevelFail(_currentLevelIndex + 1);
        StopAllCoroutines();
        StopRace();
        _curentMap.Loos();
    }

    public void Finish()
    {
        IntegrationManager.Instance.OnLevelFinish(_currentLevelIndex + 1);
        IsRaceProgress = false;
        StopAllCoroutines();
        StopRace();
        GameController.Controller.Finish(ListNameEnemies);
    }
}
