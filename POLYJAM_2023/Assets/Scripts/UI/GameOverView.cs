namespace UI
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class GameOverView : MonoBehaviour
    {

        [SerializeField]private GameObject _View;

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
            _View.SetActive(true);
        }

        public void ReplayClickHandler()
        {
            GameController.Instance.HandleReplay();
        }

        public void ExitClickHandler()
        {
            GameController.Instance.HandleExit();
        }
    }
}