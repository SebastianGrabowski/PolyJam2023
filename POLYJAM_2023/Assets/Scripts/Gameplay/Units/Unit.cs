namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Unit : MonoBehaviour
    {
        [SerializeField]protected Rigidbody2D _Rigidbody;
    
        public static List<Unit> AllUnits = new List<Unit>();

        public string DisplayName;

        public float BaseHP;
        public float HP;

        public float Attack;
        public float AttackSpeed;
        public float AttackRange;
        public float MoveSpeed;

        protected Unit _ActiveEnemy;

        public void ApplyDamage(float value)
        {
            HP = Mathf.Clamp(HP - value, 0.0f, BaseHP);
            if(HP <= 0.0f)
            {
                AllUnits.Remove(this);
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            HP = BaseHP;
            AllUnits.Add(this);
            OnStart();
        }

        protected virtual void OnStart()
        {

        }


    }
}