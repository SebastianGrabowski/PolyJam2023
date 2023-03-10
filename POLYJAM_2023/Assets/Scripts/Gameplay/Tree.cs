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
        [SerializeField]private AudioClip _HitClip;
        [SerializeField]private AudioClip _DeathSound;
        [SerializeField]private SpriteRenderer _R1;
        [SerializeField]private SpriteRenderer _R2;
        [SerializeField]private Cam _Cam;
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
             
                var shakeForce = 0.0f;
                if(_HP > (_BaseHP * 0.75f) && value <= (_BaseHP * 0.75f))
                    shakeForce = 0.25f;
                else if(_HP > (_BaseHP * 0.5f) && value <= (_BaseHP * 0.5f))
                    shakeForce = 0.5f;
                else if(_HP > (_BaseHP * 0.25f) && value <= (_BaseHP * 0.25f))
                    shakeForce = 0.75f;
                else if(_HP > 0.0f && value <= 0.0f)
                    shakeForce = 1.00f;
                
                if(shakeForce > 0.0f)
                {
                    _Cam.Shake(shakeForce);
                }

                _HP = value;
                OnChangeHP?.Invoke(diff);
                var normalized = _HP / _BaseHP;
                _Render.color = Color.Lerp(new Color(0.6f, 0.6f, 0.6f, 1.0f), Color.white, normalized);
            }
        }

        void Start()
        {
            HP = _BaseHP;
            StartCoroutine(UpdateC(_R1));
            StartCoroutine(UpdateC(_R2));
        }

        private IEnumerator UpdateC(SpriteRenderer r)
        {

            while (true)
            {
                var value = Random.Range(0.0f, 1.0f);
                var t = 0.0f;
                var maxt = Random.Range(0.1f, 2.0f);
                var finalColor = new Color(1.0f, 1.0f, 1.0f, value);
                var startColor = r.color;
                while(t < maxt)
                {
                    t += Time.deltaTime;
                    var lerp = t/maxt;
                    var color = Color.Lerp(startColor, finalColor, lerp);
                    r.color = color;
                    yield return null;
                }
                yield return null;
            }
        }

        public void ApplyDamage(float value)
        {
            HP -= value;
            if(HP <= 0.0f)
            {
                GameController.Instance.HandleGameOver();
                AudioController.Instance.PlaySound(_DeathSound);
                //Destroy(gameObject);
            } else
            {
                AudioController.Instance.PlaySound(_HitClip);
            }
            if(_DamageEffect != null)
            {
                StopCoroutine(_DamageEffect);
            }
            _DamageEffect = StartCoroutine(DamageEffect(value));
        }

        private void Update()
        {
            var gc = GameController.Instance;
            if(gc != null && !gc.Pause && !gc.IsGameOver)
            {
                HP += GameController.DT * 0.5f;
            }
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