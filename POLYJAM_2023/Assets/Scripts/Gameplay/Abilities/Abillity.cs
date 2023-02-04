using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Units;

public class Abillity : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float timeToDestroy;
    
    [Space(10)]
    [SerializeField] private Transform particleTransform;
    [SerializeField] private AbillityType abilityType;
    [HideInInspector] public float Damage;

    private List<GameObject> currentCollisions = new List<GameObject>();
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(abilityType == AbillityType.FireBall)
        {
            if(col.transform.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyUnit>().ApplyDamage(Damage);
                Destroy(gameObject);
            }
        }
        else if(abilityType == AbillityType.Thunder)
        {
            currentCollisions.Add (col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(abilityType == AbillityType.FireBall) return;
        currentCollisions.Remove(col.gameObject);
    }

    private void Awake()
    {
        if(abilityType == AbillityType.FireBall) rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToDestroy);
    }

    public void ThunderAbilitySetDamage()
    {
        foreach (var obj in currentCollisions)
        {
            var enemy = obj.GetComponent<EnemyUnit>();
            if(enemy != null) enemy.ApplyDamage(Damage);
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

    
}
