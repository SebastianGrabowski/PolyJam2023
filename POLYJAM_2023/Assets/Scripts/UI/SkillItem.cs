using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gameplay;

public class SkillItem : MonoBehaviour
{
    [SerializeField] private Sprite activeSkillLevel;
    [SerializeField] private TextMeshProUGUI skillValue;

    [Space(10)]
    [SerializeField] private Image[] levelBoxDisplay;
    [SerializeField]private AudioClip _UpgradeSound;
    
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
            if(i <= levelBoxDisplay.Length-1) levelBoxDisplay[i].sprite = activeSkillLevel;
        }   
    }

    public void UpgradeSkill()
    {
        var skillCost = godData.GetSkillByType(skillType).GetCost(godData);

        if(CurrencyController.Value >= skillCost) CurrencyController.Value -= skillCost;
        else return;

        AudioController.Instance.PlaySound(_UpgradeSound);

        var skillLevel = godData.SkillLevelUp(skillType);
        var skill = godData.GetSkillByType(skillType);

        skillValue.text = $"{skillType}: {skill.GetValue(godData)}";

        for(int i = 0; i < skillLevel + 1; i++)
        {
            if(i <= levelBoxDisplay.Length-1) levelBoxDisplay[i].sprite = activeSkillLevel;
        }
    }
}