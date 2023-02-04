using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Units;
using UnityEngine.UI;

public class GodObject : MonoBehaviour
{
    [SerializeField] private float fallVelocity;

    [SerializeField] private Image imageFill;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Animator animator;

    [Space(10)]
    [SerializeField] private SpriteRenderer idleGlowRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer rangeDisplayer;
    

    public GodData GodData { get; private set; }
    
    private Abillity currentAbillity;
    private Transform target;
    private Unit targetUnit;

    private bool isDataSet;
    private bool isMouseOver;

    private float rate;
    private float currentCooldown;
    private float timeElapsed;

    public void SetGod(GodData data, Transform transform)
    {
        GodData = data;
        target = transform;
        spriteRenderer.sprite = data.Sprite;
        idleGlowRenderer.sprite = data.IdleGlowSprite;
        imageFill.color = data.CooldownUIColor;

        isDataSet = true;
    }

    public void OnMouseOverGod()
    {
        isMouseOver = true;

        //var scale = (2 * GodData.GetSkillByType(SkillType.Range).GetValue(GodData));
        // rangeTemp.transform.localScale = new Vector3(scale, scale, rangeDisplayer.transform.localScale.z);

        spriteRenderer.sprite = GodData.HoveredSprite;
        rangeDisplayer.sprite = GodData.RangeSprites[GodData.SkillLevels[(int)SkillType.Range]];
        rangeDisplayer.gameObject.SetActive(true);
    }

    public void MouseExitGod()
    {
        isMouseOver = false;
        spriteRenderer.sprite = GodData.Sprite;
        rangeDisplayer.gameObject.SetActive(false);
    }

    void Update()
    {
        if(!isDataSet) return;

        var distance = Vector2.Distance(transform.position, target.position);
        if(distance > 0.05f)
        {   
            transform.position = Vector3.MoveTowards(transform.position, target.position, fallVelocity * Time.deltaTime);
            return;
        }
        else 
        {
            if(isMouseOver && Input.GetMouseButtonDown(0)) UIController.Instance.OnGodSelected?.Invoke(GodData);

            if(Time.time >= timeElapsed)
            {
                imageFill.fillAmount = 1f;
                targetUnit = GetNearestUnit();

                if(GodData.AbillityType == AbillityType.Thunder && targetUnit != null) ThunderAbillity();
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

            if(GodData.AbillityType == AbillityType.FireBall && currentAbillity != null && targetUnit != null) 
                currentAbillity.UpdateAbillity(targetUnit.transform);
        }
    }

    public void ThunderAbillity()
    {
        var spawnPos = Random.insideUnitCircle * GodData.GetSkillByType(SkillType.Range).GetValue(GodData);
        var abillityObj = Instantiate(GodData.Abillity.gameObject, targetUnit.transform.position, Quaternion.identity);
        currentAbillity = abillityObj.GetComponent<Abillity>();
        currentAbillity.Damage = GodData.GetSkillByType(SkillType.Damage).GetValue(GodData);

        ShowOutlineOnAbilityUse();
    }

    public void FireBallAbillity()
    {
        var abillityObj = Instantiate(GodData.Abillity.gameObject, shootingPoint.position, Quaternion.identity);
        currentAbillity = abillityObj.GetComponent<Abillity>();
        currentAbillity.Damage = GodData.GetSkillByType(SkillType.Damage).GetValue(GodData);

        ShowOutlineOnAbilityUse();
    }

    public void ShowOutlineOnAbilityUse()
    {
        animator.SetTrigger("AbillityUse");
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
