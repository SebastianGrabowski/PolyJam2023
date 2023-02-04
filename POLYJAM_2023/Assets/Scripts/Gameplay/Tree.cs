namespace Gameplay
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Rendering;

    public class Tree : MonoBehaviour
    {
        [SerializeField]private Volume _Volume;

        [SerializeField]private float _BaseHP;

        [SerializeField]private SpriteRenderer _Render;

        public UnityAction<float> OnChangeHP { get; set; }

        private Coroutine _DamageEffect;

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
            if(_DamageEffect != null)
            {
                StopCoroutine(_DamageEffect);
            }
            _DamageEffect = StartCoroutine(DamageEffect(value));
        }

        private IEnumerator DamageEffect(float value)
        {
            var startF = _Volume.weight;
            var endF = Mathf.Clamp01(startF + (value / 50.0f));
            var t = 0.0f;
            var maxt = 0.3f;
            while(t < maxt)
            {
                t += Time.deltaTime;
                _Volume.weight = Mathf.SmoothStep(startF, endF, t/maxt);
                yield return null;
            }
            while(_Volume.weight > 0.0f)
            {
                _Volume.weight -= Time.deltaTime;
                yield return null;
            }
            _Volume.weight = 0.0f;
        }
    }
}