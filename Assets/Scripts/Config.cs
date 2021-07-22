using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "R40/Config")]
public class Config : ScriptableObject
{
    public List<float> LevelDamage = new List<float>() { 10, 20, 30, 40, 50 };
    public List<float> LevelSpeed = new List<float>() { 10, 20, 30, 40, 50 };
    public List<float> LevelSize = new List<float>() { .6f, .7f, .8f, .9f, 1f };
    public List<float> LevelHealth = new List<float>() { 10, 15, 20, 25, 30};

    public Item SpawnItemArmor;
    public List<Item> ListSpawnItemsWeapon;

    public CharacterBehaviour PrefabPlauer;
    public CharacterBehaviour PrefabEnemy;

    public StatusBar StatusBarCharacter;
}
