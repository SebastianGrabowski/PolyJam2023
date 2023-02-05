using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private Image cursorImage;

    [Space(10)]
    [SerializeField] private Sprite basic;
    [SerializeField] private Sprite setOnMap;

    [Space(10)]
    [SerializeField] private GameObject objectX;

    private void Awake()
    {
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void CursorSetOnMap()
    {
        cursorImage.sprite = setOnMap;
        objectX.SetActive(true);
    }

    public void CursorSetBasic()
    {
        cursorImage.sprite = basic;
        objectX.SetActive(false);
    }
}
