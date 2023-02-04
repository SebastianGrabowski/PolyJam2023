using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Damage,
    Range,
    Rate
}

public class GodData
{
    public string Name;
    public string Description;

    public float[] Damage;
    public float[] Range;
    public float[] Rate;

    public int[] SkillLevels;

    public Sprite Sprite;
    public Sprite IconUI;
    
    public Abillity Abillity;
    public AbillityType AbillityType;

    public GodData(string name, string desc, float[] damage, float[] range, float[] rate, int[] skillLevels, Sprite sprite, Sprite iconUI, AbillityType abilityType, Abillity abillity)
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
        SkillLevels = skillLevels;
    }

    public void SkillLevelUp(SkillType skill)
    {
        if(SkillLevels[(int)skill] < 5) SkillLevels[(int)skill]++;
    }

    public float GetSkillValueBySkillType(SkillType skill)
    {
        if(skill == SkillType.Damage) return Damage[SkillLevels[(int)skill]];
        else if(skill == SkillType.Range) return Range[SkillLevels[(int)skill]];
        else if(skill == SkillType.Rate) return Rate[SkillLevels[(int)skill]];

        return 0f;
    }
}
