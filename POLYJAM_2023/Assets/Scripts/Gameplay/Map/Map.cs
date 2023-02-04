using UnityEngine;

public class Map : MonoBehaviour
{
    void Update()
    {
        //Debug.Log("IsOverUI: "+UIController.Instance.IsOverUI);
        if(!UIController.Instance.IsOverUI && Input.GetMouseButtonDown(0)) UIController.Instance.DisableGodPanel();
    }
}
