using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField]private Image _Effect;
    [SerializeField]private Fade _Fade;
    void Start()
    {
        StartCoroutine(UpdateC(_Effect));
    }

        private IEnumerator UpdateC(Image r)
        {

            while (true)
            {
                var value = Random.Range(0.0f, 1.0f);
                var t = 0.0f;
                var maxt = Random.Range(0.1f, 2.0f);
                var finalColor = new Color(1.0f, 1.0f, 1.0f, value);
                var startColor = r.color;
                while(t < maxt)
                {
                    t += Time.deltaTime;
                    var lerp = t/maxt;
                    var color = Color.Lerp(startColor, finalColor, lerp);
                    r.color = color;
                    yield return null;
                }
                yield return null;
            }
        }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            _Fade.FadeOut(()=>{UnityEngine.SceneManagement.SceneManager.LoadScene(1); });
        }
            
    }
}
