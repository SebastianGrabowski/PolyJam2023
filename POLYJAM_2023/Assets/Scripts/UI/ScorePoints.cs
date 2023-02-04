using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoints : MonoBehaviour
{
    [SerializeField]private GameObject _Template;
    [SerializeField]private AnimationCurve _CurveX;
    [SerializeField]private AnimationCurve _CurveY;
    [SerializeField]private AnimationCurve _CurveScale;
    [SerializeField]private Camera _Cam;
    public void Spawn(float delay, Vector3 pos)
    {
        pos = _Cam.WorldToScreenPoint(pos);
        StartCoroutine(UpdateFly(delay, pos));
    }
    private IEnumerator UpdateFly(float delay, Vector3 pos)
    {
        yield return new WaitForSeconds(delay);
        var newItem = Instantiate(_Template, _Template.transform.parent);
        newItem.transform.position = pos;
        newItem.gameObject.SetActive(true);
        var t = 0.0f;
        var maxt = Random.Range(1.2f, 1.6f);
        while(t < maxt)
        {
            var lerp = Mathf.SmoothStep(0.0f, 1.0f, t/maxt);
            newItem.transform.position = Vector3.Lerp(pos, transform.position, lerp);
            newItem.transform.position += new Vector3(
                _CurveX.Evaluate(lerp),
                _CurveY.Evaluate(lerp),
                0.0f) * Time.deltaTime * 50.0f;
            newItem.transform.localScale = Vector3.one * 0.3f * _CurveScale.Evaluate(lerp);
            newItem.transform.Rotate(0.0f, 0.0f, Time.deltaTime * 360.0f);
            t += Time.deltaTime;
            yield return null;
        }

        Destroy(newItem);
        Gameplay.CurrencyController.Value++;
    }
}
