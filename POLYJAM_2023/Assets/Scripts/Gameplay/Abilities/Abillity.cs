using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Units;

public class Abillity : MonoBehaviour
{
    [SerializeField]private AudioClip _Sound;

    [SerializeField] private float velocity;
    [SerializeField] private float timeToDestroy;
    
    [Space(10)]
    [SerializeField] private Transform particleTransform;
    [SerializeField] private AbillityType abilityType;

    [HideInInspector] public float GodAbillityValue;

    private List<GameObject> currentCollisions = new List<GameObject>();
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(abilityType == AbillityType.FireBall)
        {
            if(col.transform.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyUnit>().ApplyDamage(GodAbillityValue);
                Destroy(gameObject);
            }
        }
        else if(abilityType == AbillityType.Thunder)
        {
            currentCollisions.Add(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(abilityType == AbillityType.FireBall) return;
        //currentCollisions.Remove(col.gameObject);
    }

    private void Awake()
    {
        if(abilityType == AbillityType.FireBall) rb = GetComponent<Rigidbody2D>();

        if(abilityType != AbillityType.Sun) Destroy(gameObject, timeToDestroy);
    }

    public void ThunderAbilitySetDamage()
    {
        var valid = new List<EnemyUnit>();

        foreach (var obj in currentCollisions)
        {
            EnemyUnit enemy = null;
            if(obj != null) enemy = obj.GetComponent<EnemyUnit>();
            if(enemy != null) valid.Add(enemy);
        }

        foreach(var v in valid)
        {
            v.ApplyDamage(GodAbillityValue);
        }
    }

    public void UpdateAbillity(Transform targetTransform)
    {
        if(abilityType == AbillityType.FireBall && rb != null)
        {
            var dir = ((Vector2)targetTransform.position - (Vector2)transform.position).normalized;
            rb.velocity = dir * velocity;
            particleTransform.transform.rotation = transform.rotation;
        }
    }

    private void Start()
    {
        if(_Sound != null)
        {
            AudioController.Instance.PlaySound(_Sound);
        }
    }
}
