namespace UI
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class TopView : MonoBehaviour
    {

        [SerializeField]private TextMeshProUGUI _CurrencyLabel;
        [SerializeField]private TextMeshProUGUI _TreeLabel;
        [SerializeField]private TextMeshProUGUI _TimeLabel;
        [SerializeField]private TextMeshProUGUI _HealthLabel;
        [SerializeField]private AudioClip _CurrencySound;
        [SerializeField]private Image _HealthFill;
        [SerializeField]private Image _Effect;

        private int _LastCurrency;

        private Coroutine _CoroutineHP;
        private Coroutine _CoroutineCurrency;

        private void Update()
        {
            UpdateTime();
        }

        private IEnumerator UpdateX()
        {
            while (true)
            {
                var r = Random.Range(0.0f, 1.0f);
                var t = 0.0f;
                var maxt = Random.Range(0.1f, 1.0f);
                var startColor = _Effect.color;
                var endColor = new Color(1.0f, 1.0f, 1.0f, r);
                while(t < maxt)
                {
                    var c = Color.Lerp(startColor, endColor, t/maxt);
                    _Effect.color = c;
                    t+=Time.deltaTime;
                    yield return null;
                }
            }
        }

        private float _EnableTime;

        private void OnEnable()
        {
            _EnableTime = Time.timeSinceLevelLoad;
            Gameplay.CurrencyController.OnChanged += UpdateCurrency;
            var tree = FindObjectOfType<Gameplay.Tree>();
            tree.OnChangeHP += UpdateHealth;

            _LastCurrency = Gameplay.CurrencyController.Value;
            
            _CurrencyLabel.text = Gameplay.CurrencyController.Value.ToString();
            
            UpdateCurrency();
            _HealthFill.fillAmount = 1.0f;
            _HealthLabel.text = string.Format(
                "{0}/{1}",
                ((int)tree.HP).ToString(),
                ((int)tree.BaseHP).ToString());

            StartCoroutine(UpdateX());
        }

        private void OnDisable()
        {
            Gameplay.CurrencyController.OnChanged -= UpdateCurrency;
            var tree = FindObjectOfType<Gameplay.Tree>();
            if(tree != null)
            {
                tree.OnChangeHP -= UpdateHealth;
            }
        }

        private void UpdateCurrency()
        {
            if(_CoroutineCurrency != null)
            {
                StopCoroutine(_CoroutineCurrency);
            }
            
            _CoroutineCurrency = StartCoroutine(UpdateCurrencyUpdate());
        }

        private IEnumerator UpdateCurrencyUpdate()
        {
            var t = 0.0f;
            var maxt = 0.5f;
            var startV = _LastCurrency;
            var finalV = Gameplay.CurrencyController.Value;
            var playSound = startV < finalV;

            var startScale = _CurrencyLabel.transform.localScale;
            while(t < maxt)
            {
                if(t < 0.25f)
                {
                    var n = t/0.25f;
                    _CurrencyLabel.transform.localScale = Vector3.Lerp(startScale, Vector3.one * 1.3f, n);
                } else
                {
                    var n = (t-0.25f)/0.25f;
                    _CurrencyLabel.transform.localScale = Vector3.Lerp(Vector3.one * 1.3f, Vector3.one, n);
                }

                
                var lockSound = false;
                var runTime = Time.timeSinceLevelLoad - _EnableTime;
                if(runTime < 3.0f)
                    lockSound = true;

                t += Time.deltaTime;
                var v = Mathf.SmoothStep(startV, finalV, t/maxt);
                if((int)v != _LastCurrency && playSound && !lockSound)
                {
                    AudioController.Instance?.PlaySound(_CurrencySound);
                }
                _LastCurrency = (int)v;
                _CurrencyLabel.text = ((int)v).ToString();
                yield return null;
            }
            _CurrencyLabel.text = Gameplay.CurrencyController.Value.ToString();
        }

        private void UpdateTime()
        {
            var t = Gameplay.TimeController.Value;
            var interval = System.TimeSpan.FromSeconds(t);
            _TimeLabel.text = interval.ToString("mm\\:ss");
        }


        private void UpdateHealth(float diff)
        {
            var tree = FindObjectOfType<Gameplay.Tree>();
            if(tree != null)
            {
                _HealthLabel.text = string.Format(
                    "{0}<color=#3F1C1E>/{1}</color>",
                    ((int)tree.HP).ToString(),
                    ((int)tree.BaseHP).ToString());
                _HealthFill.fillAmount = tree.HP / tree.BaseHP;
                //if(_CoroutineHP != null)
                //{
                //    StopCoroutine(_CoroutineHP);
                //}
                //_CoroutineHP = StartCoroutine(UpdateHealthDiff(diff));
            }
        }

        private IEnumerator UpdateHealthDiff(float diff)
        {
            var tree = FindObjectOfType<Gameplay.Tree>();
            var startValue = _HealthFill.fillAmount;
            var finalValue = tree.HP / tree.BaseHP;
            var t = 0.0f;
            var maxt = 0.3f;
            while(t < maxt)
            {
                t += Time.deltaTime;
                var v = Mathf.SmoothStep(startValue, finalValue, t/maxt);
                _HealthFill.fillAmount =  v;
                yield return null;
            }
        }
        public void PauseClickHandler()
        {
            GameController.Instance.HandlePause();
        }
    }
}