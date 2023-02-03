using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Units;

public class Abillity : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float timeToDestroy;
    
    [Space(10)]
    [SerializeField] private AbillityType abilityType;
    [HideInInspector] public float Damage;
    
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Enemy"))
        {
            Debug.Log("other: "+other.name+" Damage: "+Damage);
            other.GetComponent<EnemyUnit>().ApplyDamage(Damage);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        if(abilityType == AbillityType.FireBall) rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToDestroy);
    }

    public void UpdateAbillity(Transform targetTransform)
    {
        if(abilityType == AbillityType.FireBall && rb != null)
        {
            var dir = ((Vector2)targetTransform.position - (Vector2)transform.position).normalized;
            rb.velocity = dir * velocity;
        }
    }

    
}
