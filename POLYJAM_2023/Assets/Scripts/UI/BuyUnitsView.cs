namespace UI
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class BuyUnitsView : MonoBehaviour
    {

        [SerializeField]private Button _Template;
        [SerializeField]private Gameplay.Units.PlayerUnit[] _Units;
        [SerializeField]private Gameplay.Units.PlayerSpawner _PlayerSpawner;

        private IEnumerator Start()
        {
            for(var i = 0; i < _Units.Length; i++)
            {
                var j = i;
                var newItem = Instantiate(_Template, _Template.transform.parent);
                newItem.gameObject.SetActive(true);
                yield return null;

                var headerLabel = newItem.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                headerLabel.text = _Units[i].DisplayName;

                var descLabel = newItem.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
                descLabel.text = _Units[i].DisplayDesc;

                var costLabel = newItem.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
                costLabel.text = _Units[i].Cost.ToString();
                
                yield return null;
                LayoutRebuilder.ForceRebuildLayoutImmediate(headerLabel.rectTransform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(descLabel.rectTransform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(costLabel.rectTransform);
                yield return null;
                LayoutRebuilder.ForceRebuildLayoutImmediate(newItem.transform.GetChild(0).GetComponent<RectTransform>());

                yield return null;

                var a = newItem.transform.GetChild(0);
                a.gameObject.SetActive(false);
                newItem.image.sprite = _Units[i].BuyIcon;
                newItem.onClick.AddListener(
                    () =>
                    {
                        var k = j;
                        _PlayerSpawner.SetUnit(_Units[k]);
                    }
                    );
            }
        }
    }
}