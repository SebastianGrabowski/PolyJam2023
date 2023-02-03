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
    
    public Abillity Abillity;
    public AbillityType AbillityType;

    public GodData(string name, string desc, float damage, float range, float rate, Sprite sprite, Sprite iconUI, AbillityType abilityType, Abillity abillity)
    {
        Name = name;
        Description = desc;
        Damage = damage;
        Range = range;
        Rate = rate;
        Sprite = sprite;
        IconUI = iconUI;
        Abillity = abillity;
        AbillityType = abilityType;
    }
}
