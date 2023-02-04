using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
	[HideInInspector] public UnityEvent<GodData> OnGodSelected;

	public GodPanel GodPanel;
	
	public void Awake()
	{
		OnGodSelected.AddListener(UpdateSelectedGod);
	}

	private void UpdateSelectedGod(GodData godData)
	{
		GodPanel.DisplayGodData(godData);
	}

	public void DisableGodPanel()
	{
		GodPanel.Disable();
	}

}