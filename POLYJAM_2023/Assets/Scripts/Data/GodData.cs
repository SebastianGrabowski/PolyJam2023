using UnityEngine;


public class GodData
{
    public int[] SkillLevels = {0, 0, 0};

    public string Name;
    public string Description;

    public float TimeToUnlock;

    public Sprite Sprite;
    public Sprite EmptySprite;
    public Sprite HoveredSprite;
    public Sprite IdleGlowSprite;
    public Sprite IconUI;
    
    public Abillity Abillity;
    public AbillityType AbillityType;

    public Color CooldownUIColor;

    public Skill[] Skills;
    public Sprite[] RangeSprites;

    public GodData(string name, string desc, float time, Sprite sprite, Sprite emptySprite, Sprite idleGlowSprite, Sprite hoveredSprite, Sprite iconUI, AbillityType abilityType, Abillity abillity, Color cooldownUIColor, Skill[] skills, Sprite[] rangeSprites)
    {
        Name = name;
        Description = desc;
        TimeToUnlock = time;
        Sprite = sprite;
        EmptySprite = emptySprite;
        IdleGlowSprite = idleGlowSprite;
        HoveredSprite = hoveredSprite;
        IconUI = iconUI;
        Abillity = abillity;
        AbillityType = abilityType;
        Skills = skills;
        CooldownUIColor = cooldownUIColor;
        RangeSprites = rangeSprites;
    }

    public int SkillLevelUp(SkillType skillType)
    {
        if(SkillLevels[(int)skillType] < 4) SkillLevels[(int)skillType]++;

        return SkillLevels[(int)skillType];
        //GetSkillByType(skillType).SkillLevel++;
        // var currentLevel = SkillLevels[(int)skill];
        // Debug.Log("SkillLeve: "+currentLevel+" skill: "+skill);
        // if(SkillLevels[(int)skill] < 4) SkillLevels[(int)skill]++;
    }

    public Skill GetSkillByType(SkillType skillType)
    {
        foreach(var skill in Skills)
        {
            if(skill.SkillType == skillType) return skill;
        }

        return null;
    }
}
