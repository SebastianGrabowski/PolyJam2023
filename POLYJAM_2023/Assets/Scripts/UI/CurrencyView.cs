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
        [SerializeField]private TextMeshProUGUI _TreeLabel;

        private void OnEnable()
        {
            Gameplay.CurrencyController.OnChanged += UpdateLabel;
            FindObjectOfType<Gameplay.Tree>().OnChangeHP += UpdateLabel;
            UpdateLabel();
        }

        private void OnDisable()
        {
            Gameplay.CurrencyController.OnChanged -= UpdateLabel;
            var tree = FindObjectOfType<Gameplay.Tree>();
            if(tree != null)
            {
                tree.OnChangeHP -= UpdateLabel;
            }
        }

        private void UpdateLabel()
        {
            _Label.text = "CURRENCY = " + Gameplay.CurrencyController.Value.ToString();
            var tree = FindObjectOfType<Gameplay.Tree>();
            if(tree != null)
            {
                _TreeLabel.text = "TREE HP = " + tree.HP + "/" + tree.BaseHP;
            }
        }
    }
}