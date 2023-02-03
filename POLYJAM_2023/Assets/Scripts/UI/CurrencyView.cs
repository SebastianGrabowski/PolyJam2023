namespace UI
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class CurrencyView : MonoBehaviour
    {

        [SerializeField]private TextMeshProUGUI _Label;

        private void OnEnable()
        {
            Gameplay.CurrencyController.OnChanged += UpdateLabel;
            UpdateLabel();
        }

        private void OnDisable()
        {
            Gameplay.CurrencyController.OnChanged -= UpdateLabel;
        }

        private void UpdateLabel()
        {
            _Label.text = "CURRENCY=" + Gameplay.CurrencyController.Value.ToString();
        }
    }
}