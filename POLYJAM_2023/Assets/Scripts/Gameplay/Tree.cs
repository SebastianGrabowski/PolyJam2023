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

        public UnityAction<float> OnChangeHP { get; set; }

        public float BaseHP => _BaseHP;

        private float _HP;
        public float HP
        {
            get => _HP;
            set
            {
                var diff = value - _HP;
                value = Mathf.Clamp(value, 0, _BaseHP);
                _HP = value;
                OnChangeHP?.Invoke(diff);
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
                GameController.Instance.HandleGameOver();
                //Destroy(gameObject);
            }
        }
    }
}