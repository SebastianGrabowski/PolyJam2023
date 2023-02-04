namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyUnit : Unit
    {

        [SerializeField]private GameObject _DeathParticles;

        public int CurrencyForKill;

        private float _AttackT;

        
        protected override void DeadHandler()
        {
            var scorePoints = FindObjectOfType<ScorePoints>();
            for(var i = 0; i < CurrencyForKill; i++)
            {
                scorePoints.Spawn(1.0f + ((float)i * 0.2f), transform.position);
            }
            if(_DeathParticles != null)
                _DeathParticles.gameObject.SetActive(true);
            Invoke(nameof(HideView), 0.5f);
            Destroy(gameObject, 2.5f);
        }

        private void HideView()
        {
            _View.gameObject.SetActive(false);
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
                _ActiveEnemy = GetNearestUnit(middleDist);
                if(_ActiveEnemy == null)
                {
                    dir = (Vector2.zero - (Vector2)transform.position).normalized;
                    var d = Vector2.Distance((Vector2)transform.position, Vector2.zero);
                    if(d <= AttackRange)
                    {
                         _AttackT += GameController.DT;
                        if(_AttackT >= AttackSpeed)
                        {
                            _AttackT = 0.0f;
                            FindObjectOfType<Gameplay.Tree>()?.ApplyDamage(Attack);
                            PlayAttackSound();
                        }
                    }
                }
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
                        PlayAttackSound();
                    }
                }
            }

            _Rigidbody.velocity = dir * MoveSpeed;
            UpdateLockMove();
            UpdateAnim();
            UpdateDir();
        }

        protected Unit GetNearestUnit(float maxDistance)
        {
            var d = float.MaxValue;
            Unit result = null;
            var pos = transform.position;
            for(var i = 0; i < AllUnits.Count; i++)
            {
                if(AllUnits[i] is PlayerUnit)
                {
                    var dd = Vector2.Distance(AllUnits[i].transform.position, pos);
                    if(dd < d)
                    {
                        d = dd;
                        result = AllUnits[i];
                    }
                }
            }

            if(d < maxDistance)
            {
                return result;
            }
            return null;
        }
    }
}