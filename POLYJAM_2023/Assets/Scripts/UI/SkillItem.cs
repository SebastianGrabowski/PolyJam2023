using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gameplay;

public class SkillItem : MonoBehaviour
{
    [SerializeField] private Sprite activeSkillLevel;
    
    [Space(10)]
    [SerializeField] private TextMeshProUGUI skillCost;
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

        skillCost.text = skill.GetCost(godData).ToString();
        skillValue.text = $"{skillType}: {skill.GetValue(godData)}";

        for(int i = 0; i < skillLevel + 1; i++)
        {
            if(i <= levelBoxDisplay.Length-1) levelBoxDisplay[i].sprite = activeSkillLevel;
        }   
    }

    public void UpgradeSkill()
    {
        var cost = godData.GetSkillByType(skillType).GetCost(godData);

        if(CurrencyController.Value >= cost) CurrencyController.Value -= cost;
        else return;

        AudioController.Instance.PlaySound(_UpgradeSound);

        var skillLevel = godData.SkillLevelUp(skillType);
        var skill = godData.GetSkillByType(skillType);

        skillCost.text = skill.GetCost(godData).ToString();
        skillValue.text = $"{skillType}: {skill.GetValue(godData)}";

        for(int i = 0; i < skillLevel + 1; i++)
        {
            if(i <= levelBoxDisplay.Length-1) levelBoxDisplay[i].sprite = activeSkillLevel;
        }
    }
}