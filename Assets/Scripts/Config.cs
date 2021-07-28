using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "R40/Config")]
public class Config : ScriptableObject
{
    public List<Map> LevelMaps;

    public List<float> LevelDamage = new List<float>() { 10, 20, 30, 40, 50 };
    public List<float> LevelSpeed = new List<float>() { 10, 20, 30, 40, 50 };
    public List<float> LevelSize = new List<float>() { .6f, .7f, .8f, .9f, 1f };
    public List<float> LevelHealth = new List<float>() { 10, 15, 20, 25, 30};

    public List<string> ListNames = new List<string>();

    public Item SpawnItemArmor;
    public float DefaultWeaponDamage;
    public List<Item> ListSpawnItemsWeapon;
    public List<Item> ListSpawnItemsWeaponTir1;
    public List<Item> ListSpawnItemsWeaponTir2;
    public List<Item> ListSpawnItemsWeaponTir3;

    public CharacterBehaviour PrefabPlayer;
    public CharacterBehaviour PrefabEnemy;

    public StatusBar StatusBarCharacter;

    public AnimationCurve RadiusRing;
    public AnimationCurve CountEnemies;

    public float GravitySimple = -1f;
    public float Gravity = -8f;

    public float TimeLifeFragment = 2f;
    public float DelayRing = 5f;
    public float SpeedRing = 3f;
    public float DamageRing = 5f;
}
