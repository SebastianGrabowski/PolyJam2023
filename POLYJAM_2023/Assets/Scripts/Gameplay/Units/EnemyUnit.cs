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

        private bool _IsDead;

        protected override void DeadHandler()
        {
            _IsDead = true;
            var scorePoints = FindObjectOfType<ScorePoints>();
            for(var i = 0; i < CurrencyForKill; i++)
            {
                scorePoints.Spawn(1.0f + ((float)i * 0.2f), transform.position);
            }
            //if(_DeathParticles != null)
            //    _DeathParticles.gameObject.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
            _Rigidbody.freezeRotation = false;
            _Rigidbody.AddTorque(360.0f);
            _Rigidbody.gravityScale = 2.0f;
            //Invoke(nameof(HideView), 1.5f);
            Destroy(gameObject, 2.5f);
        }

        private void HideView()
        {
            _View.gameObject.SetActive(false);
        }

        void Update()
        {
            if (_IsDead)
            {
                _Rigidbody.velocity = Vector2.down * 10.0f;
                var renderer = _View.GetComponent<SpriteRenderer>();
                renderer.color = Color.Lerp(renderer.color, Color.clear, Time.deltaTime * 0.5f);

                _View.localScale += (Time.deltaTime * 0.5f * Vector3.one);
                _View.localRotation = Quaternion.Lerp(_View.localRotation, Quaternion.identity, Time.deltaTime * 5.0f);
                return;
            }
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
                            FindObjectOfType<Gameplay.Tree>()?.ApplyDamage(Attack * 0.3f);
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