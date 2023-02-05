using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private GameObject cursorImage;
    [SerializeField] private GameObject cursorImageOnMap;

    [Space(10)]
    [SerializeField] private Image cursorOnMapX;

    [Space(10)]

    [SerializeField] private Sprite onMapXGreen;
    [SerializeField] private Sprite onMapXRed;

    private bool currentCanPlace = true;
    private bool isCurrentActiveOnMap = false;

    private void Awake()
    {
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetCursorOnMapAvailabity(bool canPlace)
    {
        if(canPlace == currentCanPlace && !cursorImageOnMap.activeInHierarchy) return;

        if(canPlace) cursorOnMapX.sprite = onMapXGreen;
        else cursorOnMapX.sprite = onMapXRed;

        currentCanPlace = canPlace;
    }

    public void SetCursor(bool isActiveOnMap)
    {
        if(isActiveOnMap == isCurrentActiveOnMap) return;

        cursorImage.SetActive(!isActiveOnMap);
        cursorImageOnMap.gameObject.SetActive(isActiveOnMap);
         
        isCurrentActiveOnMap = isActiveOnMap;
    }
}
