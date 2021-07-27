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

    public void LoadLevel(int levelIndex)
    {
        if(_curentMap != null)
        {
            Destroy(_curentMap.gameObject);
            _curentMap = null;
        }

        _curentMap = Instantiate(GameController.Controller.Config.LevelMaps[0], transform);
        _curentMap.Init(levelIndex);

        RadiusRing = GameController.Controller.Config.RadiusRing.Evaluate(levelIndex);
        TaperingRing.SetRadius(RadiusRing);

        GameController.Controller.ControllerUI.ShowTapToStart();
        StopAllCoroutines();
    }

    public void StartRace()
    {
        _curentMap.StartRace();
        StartCoroutine(MoveRing());
    }

    public void RemoveCharacter(CharacterBehaviour c)
    {
        _curentMap.RemoveCharacter(c);
    }

    public void ClearMap()
    {
        _curentMap.DestroyMap();
    }

    private IEnumerator MoveRing()
    {
        yield return new WaitForSeconds(Constants.DELAY_RING);

        while(RadiusRing > 0)
        {
            RadiusRing -= Time.deltaTime * Constants.SPEED_RING;
            TaperingRing.SetRadius(RadiusRing);
            yield return null;
        }
    }
}
