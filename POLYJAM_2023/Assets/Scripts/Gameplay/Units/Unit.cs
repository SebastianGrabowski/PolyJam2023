namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class Unit : MonoBehaviour
    {

        public static UnityAction<bool, float, Vector2> OnDamage { get; set; }

        [SerializeField]protected Rigidbody2D _Rigidbody;
        [SerializeField]protected Transform _View;
        [SerializeField]private float _AnimAngleMinMax;
        [SerializeField]private float _AnimAngleSpeed;
        [SerializeField]private Vector3 _AnimJumpPos;
        [SerializeField]private AudioClip _AttackSound;
        [SerializeField]private AudioClip _DeathSound;
        [SerializeField]private AudioClip _SpawnSound;
        [SerializeField]private SpriteRenderer _Glow;

        public static List<Unit> AllUnits = new List<Unit>();

        public string DisplayName;
        public string DisplayDesc;

        public float BaseHP;
        public float HP;

        public float Attack;
        public float AttackSpeed;
        public float AttackRange;
        public float MoveSpeed;

        protected Unit _ActiveEnemy;

        private int _Dir;
        public int Dir
        {
            get => _Dir;
            set
            {
                if(_Dir != value)
                {
                    _Dir = value;
                    if(value == -1)
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    else  if(value == 1)
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
            }
        }

        private bool _Dead;

        public void ApplyDamage(float value)
        {
            if(_Dead)
                return;

            HP = Mathf.Clamp(HP - value, 0.0f, BaseHP);
            if(HP <= 0.0f)
            {
                _Dead = true;
                PlayDeathSound();
                AllUnits.Remove(this);
                DeadHandler();
            }

            OnDamage?.Invoke(this is PlayerUnit, value, transform.position + (Vector3.up * 1.7f) - Vector3.forward);
        }

        protected virtual void DeadHandler() { }

        private void Start()
        {
            if(_SpawnSound != null)
            {
                AudioController.Instance.PlaySound(_SpawnSound);
            }
            HP = BaseHP;
            AllUnits.Add(this);
            OnStart();
            _GlowTime = Random.Range(0.0f, 1.0f);
        }

        protected virtual void OnStart()
        {
        }

        private float _AnimRotTime;
        private float _AnimJumpTime;

        protected void UpdateAnim()
        {
            if(_LockMove)
                return;
            _AnimRotTime += GameController.DT * _AnimAngleSpeed;
            _AnimJumpTime += GameController.DT * 10.0f;
            var lerp = Mathf.Abs(Mathf.Sin(_AnimRotTime));
            _View.rotation = Quaternion.Lerp(
                Quaternion.Euler(0.0f, 0.0f, -_AnimAngleMinMax),
                Quaternion.Euler(0.0f, 0.0f, _AnimAngleMinMax),
                lerp
                );
            var jumLerp2 = Mathf.Abs(Mathf.Sin(_AnimJumpTime));
            var jumLerp = Mathf.SmoothStep(0.0f, 1.0f, jumLerp2); 
            _View.transform.localPosition = Vector3.Lerp(Vector3.zero, _AnimJumpPos, jumLerp);
        }

        protected void UpdateDir()
        {
            if(_LockMove)
                return;
            var x = _Rigidbody.velocity.x;
            Dir = x < -0.1f ? -1 : (x > 0.1f ? 1 : 0);
        }

        private bool _LockMove;
        private float _LockMoveTime;
        private Vector3 _LockMovePos;
        protected void UpdateLockMove()
        {
            _LockMoveTime += GameController.DT;
            if(_LockMoveTime > 0.2f)
            {
                var d = Vector3.Distance(transform.position, _LockMovePos);
                _LockMove = d < 0.1f;
                _LockMovePos = transform.position;
                _LockMoveTime = 0.0f;
            }
        }

        public void PlayAttackSound() { if (_AttackSound != null) AudioController.Instance.PlaySound(_AttackSound); }
        public void PlayDeathSound() { if (_DeathSound != null) AudioController.Instance.PlaySound(_DeathSound); }

        private static Color _InvisibleWhite = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        private float _GlowTime;
        protected void UpdateGlow()
        {
            _GlowTime += GameController.DT;
            _Glow.color = Color.Lerp(_InvisibleWhite, Color.white, Mathf.Abs(Mathf.Sin(_GlowTime)));
        }
    }
}