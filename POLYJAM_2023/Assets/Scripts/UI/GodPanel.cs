using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using Gameplay;

public class GodPanel : MonoBehaviour
{
    [SerializeField] private Image iconHeader;

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
    private List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();

    void OnEnable()
    {
        CurrencyController.OnChanged += OnCurrencySpent;
    }

    void OnDisable()
    {
        CurrencyController.OnChanged -= OnCurrencySpent;
    }

    public void DisplayGodData(GodData godData)
    {
        view.SetActive(true);
        
        this.godData = godData;

        iconHeader.sprite = godData.IconUI;

        godName.color = godData.CooldownUIColor;

        godName.text = godData.Name;
        godDesc.text = godData.Description;

        for(int i = 0; i < currentItems.Count; i++)
        {
            Destroy(currentItems[i]);
        }

        upgradeButtons.Clear();
        currentItems.Clear();

        foreach(SkillType skillType in Enum.GetValues(typeof(SkillType)))
        {
            var skillItem = Instantiate(skillItemTemplate, skillItemsParent);
            skillItem.SetSkillData(godData, skillType);
            skillItem.gameObject.SetActive(true);

            var upgradeButton = skillItem.GetComponentInChildren<UpgradeButton>();
            upgradeButton.SetButtonData(this, skillType);
            upgradeButtons.Add(upgradeButton);

            currentItems.Add(skillItem.gameObject);
        }

        //LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    // public void OnSkillUpgradeHover(SkillType skillType)
    // {
    //     var cost = godData.GetSkillByType(skillType).GetCost(godData);
    //     skillCost.text = cost.ToString();
    // }

    private void OnCurrencySpent()
    {
        foreach(var button in upgradeButtons)
        {
            var skillCost = godData.GetSkillByType(button.SkillType).GetCost(godData);
            if(CurrencyController.Value < skillCost) button.GetComponent<Button>().interactable = false;
            else button.GetComponent<Button>().interactable = true;
        }
    }

    public void Disable()
    {
        view.SetActive(false);
    }
}
