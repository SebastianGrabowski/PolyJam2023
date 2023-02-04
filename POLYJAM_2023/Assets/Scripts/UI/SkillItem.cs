using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillValue;

    [Space(10)]
    [SerializeField] private Color unactive;
    [SerializeField] private Color active;

    [Space(10)]
    [SerializeField] private Image[] levelBoxDisplay;
    
    private GodData godData;
    private SkillType skillType;

    public void SetSkillData(GodData godData, SkillType skillType)
    {
        this.godData = godData;
        this.skillType = skillType;

        skillValue.text = $"{skillType}: {godData.GetSkillValueBySkillType(skillType)}";
        var skillLevel = godData.SkillLevels[(int)skillType];
        for(int i = 0; i < skillLevel; i++)
        {
            levelBoxDisplay[i].color = active;
        }   
    }

    public void UpgradeSkill()
    {
        godData.SkillLevelUp(skillType);

        skillValue.text = $"{skillType}: {godData.GetSkillValueBySkillType(skillType)}";

        var skillLevel = godData.SkillLevels[(int)skillType];
        for(int i = 0; i < skillLevel; i++)
        {
            levelBoxDisplay[i].color = active;
        }
    }
}