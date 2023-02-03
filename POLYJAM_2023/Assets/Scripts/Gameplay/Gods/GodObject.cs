using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public GodData GodData { get; private set; }
    
    private UIController controllerUI;

    private bool isMouseOver;
    private float TimeElapsed;

    private void Awake()
    {
        controllerUI = FindObjectOfType<UIController>(); 
    }

    public void SetGod(GodData data)
    {
        GodData = data;
        spriteRenderer.sprite = data.Sprite;
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }

    void Update()
    {
        if(isMouseOver && Input.GetMouseButtonDown(0)) controllerUI.OnGodSelected?.Invoke(GodData);

        if(Time.time >= TimeElapsed)
        {
            GodEffect();
            TimeElapsed = Time.time + GodData.Rate;
        }
    }

    public void GodEffect()
    {
        if(GodData.AbilityType == AbilityType.Thunder)
        {
            var spawnPos = Random.insideUnitCircle * GodData.Range;
            Instantiate(GodData.AbilityPrefab, spawnPos, Quaternion.identity);
        }
         
        // else if(GodData.AbilityType == AbilityType.FireBall)
        // {
        //     var spawnPos = Random.insideUnitCircle * GodData.Range;
        //     Instantiate(GodData.AbilityPrefab, spawnPos, Quaternion.identity);
        // }
    }

}
