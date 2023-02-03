namespace Gameplay
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class Tree : MonoBehaviour
    {

        [SerializeField]private float _BaseHP;

        [SerializeField]private SpriteRenderer _Render;

        public UnityAction OnChangeHP { get; set; }

        public float BaseHP => _BaseHP;

        private float _HP;
        public float HP
        {
            get => _HP;
            set
            {
                value = Mathf.Clamp(value, 0, _BaseHP);
                _HP = value;
                OnChangeHP?.Invoke();
                var normalized = _HP / _BaseHP;
                _Render.color = Color.Lerp(Color.black, Color.white, normalized);
            }
        }

        void Start()
        {
            HP = _BaseHP;
        }

        public void ApplyDamage(float value)
        {
            HP -= value;
            if(HP <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}