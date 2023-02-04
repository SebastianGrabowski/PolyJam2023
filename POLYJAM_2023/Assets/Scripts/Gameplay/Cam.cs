using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField]private Transform _T;

    private Coroutine _ShakeUpdate;

    public void Shake(float force)
    {
        if(_ShakeUpdate != null)
        {
            StopCoroutine(_ShakeUpdate);
        }
        _ShakeUpdate = StartCoroutine(ShakeUpdate(force));
    }

    private IEnumerator ShakeUpdate(float force)
    {
        var t = 0.0f;
        var maxt = force;
        while(t < maxt)
        {
            t += Time.deltaTime;
            var newPos = _T.transform.localPosition + (Vector3)Random.insideUnitCircle * force * Time.deltaTime * 20.0f;
            var d = Vector3.Distance(_T.transform.localPosition, Vector3.zero);
            if(d < force)
            {
                _T.transform.localPosition = newPos;
            }
            //_T.transform.localPosition += (Vector3)Random.insideUnitCircle * force * Time.deltaTime * 20.0f;
            yield return null;
        }
        t = 0.0f;
        maxt = 0.2f;
        var startPos = _T.transform.localPosition;
        while(t < maxt)
        {
            t += Time.deltaTime;
            _T.transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, Mathf.SmoothStep(0.0f, 1.0f, t/maxt));
            yield return null;
        }
            _T.transform.localPosition = Vector3.zero;
    }
}
