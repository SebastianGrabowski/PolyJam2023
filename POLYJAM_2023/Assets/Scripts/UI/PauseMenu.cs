namespace UI
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class PauseMenu : MonoBehaviour
    {

        [SerializeField]private GameObject _View;

        public static bool IsOpen;

        private IEnumerator Start()
        {
            while(GameController.Instance == null)
                yield return null;
            GameController.Instance.OnPauseMenu += Show;
        }

        private void OnDisable()
        {
            if(GameController.Instance != null)
            {
                GameController.Instance.OnPauseMenu -= Show;
            }
        }

        private void Show()
        {
            IsOpen = true;
            GameController.Instance.Pause = true;
            GetComponent<Animator>().SetTrigger("Show");
            //_View.SetActive(true);
        }
        
        public void ResumeClickHandler()
        {
            IsOpen = false;
            GameController.Instance.Pause = false;
            GetComponent<Animator>().SetTrigger("Hide");
        }
        
        public void HelperClickHandler()
        {
            GameController.Instance.HandleHelp();
            IsOpen = false;
            GetComponent<Animator>().SetTrigger("Hide");
        }

        public void ReplayClickHandler()
        {
            IsOpen = false;
            GameController.Instance.HandleReplay();
        }

        public void ExitClickHandler()
        {
            IsOpen = false;
            GameController.Instance.HandleExit();
        }
    }
}