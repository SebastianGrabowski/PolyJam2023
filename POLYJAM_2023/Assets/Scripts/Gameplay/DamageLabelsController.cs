using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageLabelsController : MonoBehaviour
{
    [SerializeField]private TextMeshPro _Template;

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
        newItem.color = isPlayer ? Color.red : Color.yellow;
        newItem.transform.position = pos;
        Destroy(newItem.gameObject, 1.5f);
    }
}
