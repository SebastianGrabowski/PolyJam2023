using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Units;
using UnityEngine.UI;

public class GodObject : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer rangeDisplayer;
    [SerializeField] private Transform shootingPoint;

    public GodData GodData { get; private set; }
    
    private Abillity currentAbillity;
    private Unit targetUnit;

    private bool isMouseOver;


    private float rate;
    private float currentCooldown;
    private float timeElapsed;

    public void SetGod(GodData data)
    {
        GodData = data;
        spriteRenderer.sprite = data.Sprite;
        imageFill.color = data.CooldownUIColor;
    }

    public void OnMouseOverGod()
    {
        isMouseOver = true;

        //var scale = (2 * GodData.GetSkillByType(SkillType.Range).GetValue(GodData));
        // rangeTemp.transform.localScale = new Vector3(scale, scale, rangeDisplayer.transform.localScale.z);
        rangeDisplayer.sprite = GodData.RangeSprites[GodData.SkillLevels[(int)SkillType.Range]];
        rangeDisplayer.gameObject.SetActive(true);
    }

    public void MouseExitGod()
    {
        isMouseOver = false;
        rangeDisplayer.gameObject.SetActive(false);
    }

    void Update()
    {
        if(isMouseOver && Input.GetMouseButtonDown(0)) UIController.Instance.OnGodSelected?.Invoke(GodData);

        if(Time.time >= timeElapsed)
        {
            imageFill.fillAmount = 1f;
            targetUnit = GetNearestUnit();

            if(GodData.AbillityType == AbillityType.Thunder) ThunderAbillity();
            else if(GodData.AbillityType == AbillityType.FireBall && targetUnit != null) FireBallAbillity();

            rate = GodData.GetSkillByType(SkillType.Rate).GetValue(GodData);
            currentCooldown = rate;
            
            timeElapsed = Time.time + GodData.GetSkillByType(SkillType.Rate).GetValue(GodData);
        }
        else
        {
            currentCooldown -= Time.deltaTime;
            imageFill.fillAmount = currentCooldown / rate;
        }

        if(GodData.AbillityType == AbillityType.FireBall && currentAbillity != null && targetUnit != null) currentAbillity.UpdateAbillity(targetUnit.transform);
    }

    public void ThunderAbillity()
    {
        if(GodData.AbillityType == AbillityType.Thunder)
        {
            var spawnPos = Random.insideUnitCircle * GodData.GetSkillByType(SkillType.Range).GetValue(GodData);
            var abillityObj = Instantiate(GodData.Abillity.gameObject, spawnPos, Quaternion.identity);
        }
    }

    public void FireBallAbillity()
    {
        //var spawnPos = Random.insideUnitCircle * GodData.Range;
        var abillityObj = Instantiate(GodData.Abillity.gameObject, shootingPoint.position, Quaternion.identity);
        currentAbillity = abillityObj.GetComponent<Abillity>();
        currentAbillity.Damage = GodData.GetSkillByType(SkillType.Damage).GetValue(GodData);
    }

    private Unit GetNearestUnit()
    {
        var pos = transform.position;
        for(var i = 0; i < Unit.AllUnits.Count; i++)
        {
            if(Unit.AllUnits[i] is EnemyUnit)
            {
                var dd = Vector2.Distance(Unit.AllUnits[i].transform.position, pos);
                if(dd <= GodData.GetSkillByType(SkillType.Range).GetValue(GodData))
                {
                    return Unit.AllUnits[i];
                }
            }
        }

        return null;
    }


}
