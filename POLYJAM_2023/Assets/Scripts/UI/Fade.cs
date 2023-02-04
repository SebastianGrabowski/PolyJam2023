using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField]private UnityEngine.UI.RawImage _RI;

    // Start is called before the first frame update

    private IEnumerator Start()
    {
        _RI.material.SetFloat("_Value", 1.0f);
        yield return new WaitForSeconds(1.0f);
        var t = 0.0f;
        var maxt = 1.0f;
        while(t < maxt)
        {
            t += Time.deltaTime;
            var v = Mathf.SmoothStep(1.0f, 0.0f, t/maxt);
            _RI.material.SetFloat("_Value", v);
            yield return null;
        }
        _RI.material.SetFloat("_Value", 0.0f);
        _RI.enabled = false;
    }

    public void FadeOut(UnityEngine.Events.UnityAction onFinish)
    {
        StartCoroutine(UpdateFade(onFinish));
    }

    private IEnumerator UpdateFade(UnityEngine.Events.UnityAction onFinish)
    {
        _RI.enabled = true;
        var t = 0.0f;
        var maxt = 1.0f;
        while(t < maxt)
        {
            t += Time.deltaTime;
            var v = Mathf.SmoothStep(0.0f, 1.0f, t/maxt);
            _RI.material.SetFloat("_Value", v);
            yield return null;
        }
        _RI.material.SetFloat("_Value", 1.0f);
        onFinish?.Invoke();
    }
}
