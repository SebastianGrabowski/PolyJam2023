using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private GameObject cursorImage;
    [SerializeField] private GameObject cursorImageOnMap;

    private bool isCurrentActiveOnMap = false;

    private void Awake()
    {
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetCursor(bool isActiveOnMap)
    {
        if(isActiveOnMap == isCurrentActiveOnMap) return;

        cursorImage.SetActive(!isActiveOnMap);
        cursorImageOnMap.SetActive(isActiveOnMap);
         
        isCurrentActiveOnMap = isActiveOnMap;
    }
}
