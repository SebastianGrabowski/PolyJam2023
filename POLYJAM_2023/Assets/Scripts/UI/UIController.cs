using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
	private static UIController instance;
    public static UIController Instance { get { return instance; } }

	private bool canDisablePanel;

    private void Awake()
    {
        if(instance != null && instance != this) Destroy(this.gameObject); 
		else instance = this;

		OnGodSelected.AddListener(UpdateSelectedGod);
    }

	[HideInInspector] public UnityEvent<GodData> OnGodSelected;
	public GodPanel GodPanel;
	public bool IsOverUI;

	private void UpdateSelectedGod(GodData godData)
	{
		canDisablePanel = false;
		StopCoroutine(SetDisability());
		GodPanel.DisplayGodData(godData);

		StartCoroutine(SetDisability());
	}

	public IEnumerator SetDisability()
	{
		yield return new WaitForSeconds(0.25f);
		canDisablePanel = true;
	}

	public void DisableGodPanel()
	{
		if(GodPanel.view.activeInHierarchy && canDisablePanel)
		{
			GodPanel.Disable();
			canDisablePanel = false;
		}
	}

}