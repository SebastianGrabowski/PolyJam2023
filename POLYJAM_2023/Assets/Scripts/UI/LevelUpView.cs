namespace UI
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class LevelUpView : MonoBehaviour
    {

        [SerializeField]private GameObject _View;
        [SerializeField]private GameObject _Label;
        [SerializeField]private CanvasGroup _Cg;

        [SerializeField]private Image _EnemyImage;
        [SerializeField]private Sprite[] _Enemies;
        [SerializeField]private TextMeshProUGUI _NewUnitLabel;
        [SerializeField]private TextMeshProUGUI _NewGodLabel;
        [SerializeField]private TextMeshProUGUI _NewEnemyLabel;

        private bool _Visible;
        private int _Lvl;
        private int _LastLVL;

        private void Update()
        {
            if (!_Visible)
            {
                var v = Gameplay.TimeController.Value;
                _Lvl= 0;
                if(v == 59)
                {
                    _Lvl = 1;
                } else if (v == 119)
                {
                    _Lvl = 2;

                } else if (v == 179)
                {
                    _Lvl = 3;

                }

                if(_LastLVL < _Lvl && _Lvl != 0)
                {
                    _LastLVL = _Lvl;
                    SetUp();
                    StartCoroutine(ShowUpdate());
                    _Visible = true;
                }
            }
        }

        private IEnumerator ShowUpdate()
        {
            GameController.Instance.Pause = true;
            _View.SetActive(true);
            _Cg.alpha = 0.0f;
            GetComponent<Animator>().SetTrigger("Show");
            yield return new WaitForSeconds(1.5f);
            _Label.SetActive(true);
            while (!Input.anyKey)
            {
                yield return null;
            }
            _Cg.alpha = 0.0f;
            GetComponent<Animator>().SetTrigger("Hide");
            yield return new WaitForSeconds(1.1f);
            _View.SetActive(false);
            _Visible = false;
            GameController.Instance.Pause = false;
        }

        private void SetUp()
        {
            if(_Lvl == 1)
            {
                _NewGodLabel.text = "Idol statue - Perun - is now available!";
                _NewUnitLabel.transform.parent.gameObject.SetActive(true);
                _NewUnitLabel.text = "New defensive unit - Baba Jaga - unlocked!";
                _NewEnemyLabel.transform.parent.gameObject.SetActive(true);
                _NewEnemyLabel.text = "Enemy Elves are comming!";
                _EnemyImage.enabled = true;
                _EnemyImage.sprite = _Enemies[1];
            }
            
            if(_Lvl == 2)
            {
                _NewGodLabel.text = "Idol statue - Dadźbog - is now available!";
                _NewUnitLabel.transform.parent.gameObject.SetActive(true);
                _NewUnitLabel.text = "New defensive unit - Wampierz - unlocked";
                _NewEnemyLabel.transform.parent.gameObject.SetActive(true);
                _NewEnemyLabel.text = "Frightening Giants are comming!";
                _EnemyImage.enabled = true;
                _EnemyImage.sprite = _Enemies[2];
            }

            if(_Lvl == 3)
            {
                _NewGodLabel.text = "Idol statue - Weles - is now available!";
                _NewUnitLabel.transform.parent.gameObject.SetActive(false);
                _NewEnemyLabel.transform.parent.gameObject.SetActive(false);
                _NewEnemyLabel.text = "ENEMY 3";
                _EnemyImage.enabled = false;
            }
        }
    }
}