using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Damage,
    Range,
    Rate
}

[System.Serializable]
public class Skill
{
    public SkillType SkillType;
   
    [Space(10)]
    public float[] Value;
    public float[] SkillCost;

    public float GetValue(GodData godData)
    {
        return Value[godData.SkillLevels[(int)SkillType]];
    }

    public int GetCost(GodData godData)
    {
        return (int)SkillCost[godData.SkillLevels[(int)SkillType]];
    }
}