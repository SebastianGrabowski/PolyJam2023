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

        public static List<Unit> AllUnits = new List<Unit>();

        public string DisplayName;

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

        public void ApplyDamage(float value)
        {
            HP = Mathf.Clamp(HP - value, 0.0f, BaseHP);
            if(HP <= 0.0f)
            {
                AllUnits.Remove(this);
                DeadHandler();
                Destroy(gameObject);
            }

            OnDamage?.Invoke(this is PlayerUnit, value, transform.position + (Vector3.up * 0.7f));
        }

        protected virtual void DeadHandler() { }

        private void Start()
        {
            HP = BaseHP;
            AllUnits.Add(this);
            OnStart();
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
            _AnimRotTime += Time.deltaTime * _AnimAngleSpeed;
            _AnimJumpTime += Time.deltaTime * 10.0f;
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
            _LockMoveTime += Time.deltaTime;
            if(_LockMoveTime > 0.2f)
            {
                var d = Vector3.Distance(transform.position, _LockMovePos);
                _LockMove = d < 0.1f;
                _LockMovePos = transform.position;
                _LockMoveTime = 0.0f;
            }
        }
    }
}