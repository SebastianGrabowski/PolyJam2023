using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageLabelsController : MonoBehaviour
{
    [SerializeField]private TextMeshPro _Template;
    [SerializeField]private Color _C;

    private void OnEnable()
    {
        Gameplay.Units.Unit.OnDamage += SpawnDamageLabel;
    }

    private void OnDisable()
    {
        Gameplay.Units.Unit.OnDamage -= SpawnDamageLabel;
    }

    private void SpawnDamageLabel(bool isPlayer, float value, Vector2 pos)
    {
        var newItem = Instantiate(_Template);
        newItem.gameObject.SetActive(true);
        newItem.text = ((int)value).ToString();
        newItem.color = isPlayer ? _C : Color.yellow;
        newItem.transform.position = pos + Random.insideUnitCircle;
        Destroy(newItem.gameObject, 1.5f);
    }
}
