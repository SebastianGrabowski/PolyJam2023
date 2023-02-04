using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler
{
    private GodPanel godPanel;
    public SkillType SkillType { get; private set; }

    public void SetButtonData(GodPanel godPanel, SkillType skillType)
    {
        this.godPanel = godPanel;
        this.SkillType = skillType;
    }

    public void OnButtonClick()
    {
        godPanel.OnSkillUpgradeHover(SkillType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        godPanel.OnSkillUpgradeHover(SkillType);
    }
}