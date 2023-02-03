namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Unit : MonoBehaviour
    {
        [SerializeField]protected Rigidbody2D _Rigidbody;
    
        public static List<Unit> AllUnits = new List<Unit>();

        public float BaseHP;
        public float HP;

        public float Attack;
        public float AttackSpeed;
        public float AttackRange;
        public float MoveSpeed;


        public void ApplyDamage(float value)
        {
            BaseHP = Mathf.Clamp(BaseHP + value, 0.0f, BaseHP);
            if(BaseHP <= 0.0f)
            {
                AllUnits.Remove(this);
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            AllUnits.Add(this);
            OnStart();
        }

        protected virtual void OnStart()
        {

        }


    }
}