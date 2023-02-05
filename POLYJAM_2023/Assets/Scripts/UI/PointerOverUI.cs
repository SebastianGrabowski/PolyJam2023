using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UIController controllerUI;

    public void Awake()
	{
		controllerUI = UIController.Instance;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIController.Instance.IsOverUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIController.Instance.IsOverUI = false;
    }
}