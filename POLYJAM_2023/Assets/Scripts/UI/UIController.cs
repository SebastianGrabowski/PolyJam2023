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
		Debug.Log("UpdateSelectedGod: "+godData.Name);
		GodPanel.DisplayGodData(godData);
		Invoke(nameof(SetDisability), 0.25f);
	}

	private void SetDisability()
	{
		canDisablePanel = true;
	}

	public void DisableGodPanel()
	{
		if(GodPanel.view.activeInHierarchy && canDisablePanel)
		{
			Debug.Log("Wylaczam panel");
			 GodPanel.Disable();
			 canDisablePanel = false;
		}
	}

}