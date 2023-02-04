namespace UI
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class HelpView : MonoBehaviour
    {

        public static bool IsOpen;

        [SerializeField]private RectTransform[] _RectsToRebuild;

        private IEnumerator Start()
        {
            while(GameController.Instance == null)
                yield return null;
            GameController.Instance.OnHelp += Show;
        }

        private void OnDisable()
        {
            if(GameController.Instance != null)
            {
                GameController.Instance.OnHelp -= Show;
            }
        }

        private void Show()
        {
            IsOpen = true;
            GameController.Instance.Pause = true;
            GetComponent<Animator>().SetTrigger("Show");

            foreach(var r in _RectsToRebuild)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(r);
            }
        }
        
        public void Close()
        {
            IsOpen = false;
            GameController.Instance.Pause = false;
            GetComponent<Animator>().SetTrigger("Hide");
        }
    }
}