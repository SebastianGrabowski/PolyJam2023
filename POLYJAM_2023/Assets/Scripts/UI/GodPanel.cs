using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class GodPanel : MonoBehaviour
{
    [SerializeField] private Image image;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI godName;
    [SerializeField] private TextMeshProUGUI godDesc;
    [SerializeField] private TextMeshProUGUI skillCost;

    [Space(10)]
    public GameObject view;

    [Space(10)]
    [SerializeField] private RectTransform rectTransform;

    [Space(10)]
    [SerializeField] private Transform skillItemsParent;
    [SerializeField] private SkillItem skillItemTemplate;

    private GodData godData;
    private List<GameObject> currentItems = new List<GameObject>();

    public void DisplayGodData(GodData godData)
    {
        view.SetActive(true);
        
        this.godData = godData;

        image.sprite = godData.IconUI;
        godName.text = godData.Name;
        godDesc.text = godData.Description;

        for(int i = 0; i < currentItems.Count; i++)
        {
            Destroy(currentItems[i]);
        }

        currentItems.Clear();

        foreach(SkillType skillType in Enum.GetValues(typeof(SkillType)))
        {
            var skillItem = Instantiate(skillItemTemplate, skillItemsParent);
            skillItem.SetSkillData(godData, skillType);
            skillItem.gameObject.SetActive(true);

            skillItem.GetComponentInChildren<UpgradeButton>().SetButtonData(this, skillType);

            currentItems.Add(skillItem.gameObject);
        }

        //LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void OnSkillUpgradeHover(SkillType skillType)
    {
        var cost = godData.GetSkillByType(skillType).GetCost(godData);
        skillCost.text = "Skill cost: "+cost+" currency";
    }

    public void Disable()
    {
        view.SetActive(false);
    }
}
