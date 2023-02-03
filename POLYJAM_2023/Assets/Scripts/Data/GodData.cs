using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodData
{
    public string Name;
    public string Description;

    public float Damage;
    public float Range;
    public float Rate;

    public Sprite Sprite;
    public Sprite IconUI;

    public GameObject AbilityPrefab;
    public AbilityType AbilityType;

    public GodData(string name, string desc, float damage, float range, float rate, Sprite sprite, Sprite iconUI, AbilityType abilityType, GameObject abilityPrefab)
    {
        Name = name;
        Description = desc;
        Damage = damage;
        Range = range;
        Rate = rate;
        Sprite = sprite;
        IconUI = iconUI;
        AbilityPrefab = abilityPrefab;
        AbilityType = abilityType;
    }
}
