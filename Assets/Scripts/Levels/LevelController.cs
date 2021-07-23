using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Transform ParentItems => _curentMap.ParentItems;
    public Transform PatentFragment => _curentMap.PatentFragment;
    public Transform PatentCharacters => _curentMap.PatentCharacters;

    private Map _curentMap;

    public void LoadLevel(int level)
    {
        if(_curentMap != null)
        {
            Destroy(_curentMap.gameObject);
            _curentMap = null;
        }

        _curentMap = Instantiate(GameController.Controller.Config.LevelMaps[0], transform);
        _curentMap.Init(level);
    }

    public void RemoveCharacter(CharacterBehaviour c)
    {
        _curentMap.RemoveCharacter(c);
    }

    public void ClearMap()
    {
        _curentMap.DestroyMap();
    }
}
