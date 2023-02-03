using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Units;

public class GodObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public GodData GodData { get; private set; }
    
    private UIController controllerUI;
    private Abillity currentAbillity;
    private Unit targetUnit;

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
            if(GodData.AbillityType == AbillityType.Thunder) ThunderAbillity();
            else if(GodData.AbillityType == AbillityType.FireBall) FireBallAbillity();
            
            targetUnit = GetNearestUnit();
            TimeElapsed = Time.time + GodData.Rate;
        }

        if(GodData.AbillityType == AbillityType.FireBall && currentAbillity != null && targetUnit != null) currentAbillity.UpdateAbillity(targetUnit.transform);
    }

    public void ThunderAbillity()
    {
        if(GodData.AbillityType == AbillityType.Thunder)
        {
            var spawnPos = Random.insideUnitCircle * GodData.Range;
            var abillityObj = Instantiate(GodData.Abillity.gameObject, spawnPos, Quaternion.identity);
        }
    }

    public void FireBallAbillity()
    {
        //var spawnPos = Random.insideUnitCircle * GodData.Range;
        var abillityObj = Instantiate(GodData.Abillity.gameObject, transform.position, Quaternion.identity);
        currentAbillity = abillityObj.GetComponent<Abillity>();
        currentAbillity.Damage = GodData.Damage;
    }

    private Unit GetNearestUnit()
    {
        var d = float.MaxValue;
        Unit result = null;
        var pos = transform.position;
        for(var i = 0; i < Unit.AllUnits.Count; i++)
        {
            if(Unit.AllUnits[i] is EnemyUnit)
            {
                var dd = Vector2.Distance(Unit.AllUnits[i].transform.position, pos);
                if(dd < d)
                {
                    d = dd;
                    result = Unit.AllUnits[i];
                }
            }
        }

        return result;
    }


}
