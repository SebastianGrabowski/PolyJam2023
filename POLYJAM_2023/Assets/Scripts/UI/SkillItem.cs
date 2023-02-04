using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gameplay;

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

        var skill = godData.GetSkillByType(skillType);
        var skillLevel = godData.SkillLevels[(int)skillType];

        skillValue.text = $"{skillType}: {skill.GetValue(godData)}";

        for(int i = 0; i < skillLevel + 1; i++)
        {
            if(i <= levelBoxDisplay.Length-1) levelBoxDisplay[i].color = active;
        }   
    }

    public void UpgradeSkill()
    {
        var skillCost = godData.GetSkillByType(skillType).GetCost(godData);

        if(CurrencyController.Value >= skillCost) CurrencyController.Value -= skillCost;
        else return;

        var skillLevel = godData.SkillLevelUp(skillType);
        var skill = godData.GetSkillByType(skillType);

        skillValue.text = $"{skillType}: {skill.GetValue(godData)}";

        for(int i = 0; i < skillLevel + 1; i++)
        {
            if(i <= levelBoxDisplay.Length-1) levelBoxDisplay[i].color = active;
        }
    }
}