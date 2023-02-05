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

        private bool _Visible;

        private void Update()
        {
            if (!_Visible)
            {
                //Gameplay.TimeController.Value 
            }
        }

        private IEnumerator Start()
        {
            while(GameController.Instance == null)
                yield return null;
            GameController.Instance.OnGameOver += ShowGameOver;
        }

        private void OnDisable()
        {
            if(GameController.Instance != null)
            {
                GameController.Instance.OnGameOver -= ShowGameOver;
            }
        }

        private void ShowGameOver()
        {
            GetComponent<Animator>().SetTrigger("Show");
            //_View.SetActive(true);
        }


        public void ExitClickHandler()
        {
            GameController.Instance.HandleExit();
        }
    }
}