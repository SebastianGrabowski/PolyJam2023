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

        private void OnEnable()
        {
            Gameplay.CurrencyController.OnChanged += UpdateCurrency;
            FindObjectOfType<Gameplay.Tree>().OnChangeHP += UpdateHealth;
            UpdateCurrency();
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
            _CurrencyLabel.text = Gameplay.CurrencyController.Value.ToString();
        }

        private void UpdateTime()
        {
            _TimeLabel.text = Random.Range(10, 100).ToString();
        }


        private void UpdateHealth(float diff)
        {
            var tree = FindObjectOfType<Gameplay.Tree>();
            if(tree != null)
            {
                _TreeLabel.text = "TREE HP = " + tree.HP + "/" + tree.BaseHP;
            }
        }

        public void PauseClickHandler()
        {
            GameController.Instance.HandlePause();
        }
    }
}