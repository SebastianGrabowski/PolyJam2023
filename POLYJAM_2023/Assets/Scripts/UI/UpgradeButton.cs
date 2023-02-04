using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler
{
    private GodPanel godPanel;
    private SkillType skillType;

    public void SetButtonData(GodPanel godPanel, SkillType skillType)
    {
        this.godPanel = godPanel;
        this.skillType = skillType;
    }

    public void OnButtonClick()
    {
        godPanel.OnSkillUpgradeHover(skillType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        godPanel.OnSkillUpgradeHover(skillType);
    }
}