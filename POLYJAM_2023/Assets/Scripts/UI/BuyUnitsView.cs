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

        [SerializeField]private Sprite[] _UnlockedIcons;
        [SerializeField]private Sprite[] _LockedIcons;


        private List<Button> _Buttons = new List<Button>();

        private List<Image> _Connectors = new List<Image>();

        private God[] Gods;
        private bool ready;

        private void Update()
        {
            if (ready)
            {
               var activeUnit = _PlayerSpawner.ActiveUnit;

                var time = Gameplay.TimeController.Value;
                for(var i = 0; i < _Buttons.Count; i++)
                {
                    for(var j = 0; j < Gods.Length; j++)
                    {
                        if (Gods[j].ID == i)
                        {
                            var unlocked = time >= Gods[j].TimeToUnlock;
                            _Buttons[i].image.sprite = unlocked ? _Units[i].IconUnlocked : _Units[i].IconLocked;
                            _Connectors[i].gameObject.SetActive(activeUnit == _Units[i]);
                            _Buttons[i].interactable = unlocked;
                            _Buttons[i].image.raycastTarget = unlocked;
                        }
                    }
                }
            }
        }

        private IEnumerator Start()
        {
            ready = false;
            for(var i = 0; i < _Units.Length; i++)
            {
                var j = i;
                var newItem = Instantiate(_Template, _Template.transform.parent);
                newItem.gameObject.SetActive(true);
                yield return null;

                _Buttons.Add(newItem);

                var headerLabel = newItem.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                headerLabel.text = _Units[i].DisplayName;

                var costLabel = newItem.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
                costLabel.text = _Units[i].Cost.ToString();
                
                var connector = newItem.transform.GetChild(1).GetComponent<Image>();
                _Connectors.Add(connector);

                var a = newItem.transform.GetChild(0);
                a.gameObject.SetActive(false);
                //newItem.image.sprite = _Units[i].BuyIcon;
                newItem.onClick.AddListener(
                    () =>
                    {
                        var k = j;
                        _PlayerSpawner.SetUnit(_Units[k]);
                    }
                    );
            }
            Gods = Resources.LoadAll<God>("");
            ready = true;
        }
    }
}