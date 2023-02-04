using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
	private static UIController instance;
    public static UIController Instance { get { return instance; } }

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
		GodPanel.DisplayGodData(godData);
	}

	public void DisableGodPanel()
	{
		if(GodPanel.view.activeInHierarchy) GodPanel.Disable();
	}

}