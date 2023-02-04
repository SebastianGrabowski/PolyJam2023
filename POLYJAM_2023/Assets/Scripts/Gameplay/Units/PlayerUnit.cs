namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerUnit : Unit
    {
        [SerializeField] public GameObject BuffIcon;
        public Sprite BuyIcon;
        public int Cost;
        public int GodOwnerID;
        
        private float _AttackT;

        private float attackBeforeBuff;
        
        protected override void DeadHandler()
        {
            //CurrencyController.Value += CurrencyForKill;
           // _DeathParticles.gameObject.SetActive(true);
            Destroy(gameObject, 1.0f);
        }

        void Update()
        {
            if (GameController.Instance.IsGameOver || GameController.Instance.Pause)
            {
                _Rigidbody.velocity = Vector2.zero;
                return;
            }

            UpdateGlow();

            var middleDist = Vector2.Distance(transform.position, Vector2.zero);
            
            var dir = Vector2.zero;

            if(_ActiveEnemy == null)
            {
                _ActiveEnemy = GetNearestUnit();
            } else
            {
                dir = ((Vector2)_ActiveEnemy.transform.position - (Vector2)transform.position).normalized;
                var d = Vector2.Distance(_ActiveEnemy.transform.position, transform.position);
                if(d <= AttackRange)
                {
                    _AttackT += GameController.DT;
                    if(_AttackT >= AttackSpeed)
                    {
                        _AttackT = 0.0f;
                        _ActiveEnemy.ApplyDamage(Attack);
                    }
                }
            }

            _Rigidbody.velocity = dir * MoveSpeed;
            UpdateLockMove();
            UpdateAnim();
            UpdateDir();
        }

        public void AddBuff(float value)
        {
            BuffIcon.SetActive(true);
            attackBeforeBuff = Attack;
            Attack += value;

            StartCoroutine(BackToNormal());
        }

        private IEnumerator BackToNormal()
        {
            yield return new WaitForSeconds(5f);
            BuffIcon.SetActive(false);
            Attack = attackBeforeBuff;
        }

        protected Unit GetNearestUnit()
        {
            var d = float.MaxValue;
            Unit result = null;
            var pos = transform.position;
            for(var i = 0; i < AllUnits.Count; i++)
            {
                if(AllUnits[i] is EnemyUnit)
                {
                    var dd = Vector2.Distance(AllUnits[i].transform.position, pos);
                    if(dd < d)
                    {
                        d = dd;
                        result = AllUnits[i];
                    }
                }
            }

            return result;
        }
    }
}