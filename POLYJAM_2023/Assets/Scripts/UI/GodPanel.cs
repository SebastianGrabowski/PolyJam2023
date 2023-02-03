using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GodPanel : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private Image image;

    [SerializeField] private TextMeshProUGUI godName;
    [SerializeField] private TextMeshProUGUI godDesc;

    [SerializeField] private GameObject view;

    public void DisplayGodData(GodData godData)
    {
        view.SetActive(true);
        
        image.sprite = godData.IconUI;
        godName.text = godData.Name;
        godDesc.text = godData.Description;
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
