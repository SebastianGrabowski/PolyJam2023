using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class GodPanel : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private Image image;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI godName;
    [SerializeField] private TextMeshProUGUI godDesc;

    [Space(10)]
    [SerializeField] private GameObject view;

    [Space(10)]
    [SerializeField] private RectTransform rectTransform;

    [Space(10)]
    [SerializeField] private Transform skillItemsParent;
    [SerializeField] private SkillItem skillItemTemplate;

    private List<GameObject> currentItems = new List<GameObject>();

    public void DisplayGodData(GodData godData)
    {
        view.SetActive(true);
        
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

            currentItems.Add(skillItem.gameObject);
        }

        //LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void Disable()
    {
        view.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        view.SetActive(false);
    }
}
