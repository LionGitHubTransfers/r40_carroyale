using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Serializable]
    public struct ObstacleCellGroup
    {
        public Transform[] Obstacles;
    }

    public Transform ParentItems;
    public Transform PatentFragment;
    public Transform PatentCharacters;

    public List<ObstacleCellGroup> ObstacleGroups;
    public List<Transform> ListSpawnPoints;

    private List<CharacterBehaviour> _characters;

    public void Init(int level)
    {
        foreach (ObstacleCellGroup cells in ObstacleGroups)
        {
            int countObstaclesCells = cells.Obstacles.Length;

            Vector3[] randomPosition = new Vector3[countObstaclesCells];

            for (int i = 0; i < countObstaclesCells; i++)
                randomPosition[i] = cells.Obstacles[i].position;


            int n = countObstaclesCells;

            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                var value = randomPosition[k];
                randomPosition[k] = randomPosition[n];
                randomPosition[n] = value;
            }

            for (int i = 0; i < countObstaclesCells; i++)
                cells.Obstacles[i].position = randomPosition[i];
        }

        //int[] randomIndex = new int[ListSpawnPoints.Count];

        //for (int i = 0; i < ListSpawnPoints.Count; i++)
        //    randomIndex[i] = i;

        //int nn = ListSpawnPoints.Count;

        //while (nn > 1)
        //{
        //    nn--;
        //    int k = UnityEngine.Random.Range(0, nn + 1);
        //    var value = randomIndex[k];
        //    randomIndex[k] = randomIndex[nn];
        //    randomIndex[nn] = value;
        //}
        int tempCountCharacher = 2 + 1;

        int[] randomIndex = GetRandomIndex(ListSpawnPoints.Count);
        int[] randomIndexNames = GetRandomIndex(GameController.Controller.Config.ListNames.Count);

        _characters = new List<CharacterBehaviour>();

        for (int i=0; i < tempCountCharacher; i++)
        {
            CharacterBehaviour character;
            string name;
            if (i == 0)
            {
                name = Constants.TAG_PLAYER;
                character = Instantiate(GameController.Controller.Config.PrefabPlayer, ListSpawnPoints[randomIndex[i]].position, Quaternion.identity, PatentCharacters);
            }
            else
            {
                name = GameController.Controller.Config.ListNames[randomIndexNames[i]];
                character = Instantiate(GameController.Controller.Config.PrefabEnemy, ListSpawnPoints[randomIndex[i]].position, Quaternion.identity, PatentCharacters);
            }

            character.Init(name);
            _characters.Add(character);
        }
    }

    private int[] GetRandomIndex(int count)
    {
        int[] randomIndex = new int[count];

        for (int i = 0; i < count; i++)
            randomIndex[i] = i;

        int nn = count;

        while (nn > 1)
        {
            nn--;
            int k = UnityEngine.Random.Range(0, nn + 1);
            var value = randomIndex[k];
            randomIndex[k] = randomIndex[nn];
            randomIndex[nn] = value;
        }
        return randomIndex;
    }

    public void StartRace()
    {
        foreach (CharacterBehaviour c in _characters)
        {
            c.StartRace();
        }
    }

    public void StopRace()
    {
        foreach (CharacterBehaviour c in _characters)
        {
            c.StopRace();
        }
    }

    public void RemoveCharacter(CharacterBehaviour c)
    {
        if (_characters.Exists(x => x == c))
            _characters.Remove(c);
    }

    public void DestroyMap()
    {
        foreach(CharacterBehaviour c in _characters)
        {
            c.DestroyStatusBar();
            //if(c.IsLife)
            //    c.DestroyCgaracter();
        }

        Destroy(gameObject);
    }

    public void Loos()
    {

    }
}
