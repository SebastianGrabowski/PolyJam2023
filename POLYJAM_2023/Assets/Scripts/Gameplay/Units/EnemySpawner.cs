namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField]private EnemyUnit[] _Units;
        [SerializeField]private float _FromTime;
        [SerializeField]private float _ToTime;
        [SerializeField]private float _IntervalMin;
        [SerializeField]private float _IntervalMax;
        [SerializeField]private int _MinCount;
        [SerializeField]private int _MaxCount;

        private float _Time;

        void Update()
        {
            if(GameController.Instance.IsGameOver)
                return;

            if(TimeController.Value < _FromTime)
                return;

            if(TimeController.Value > _ToTime)
                return;

            _Time -= GameController.DT;
            if(_Time <= 0.0f)
            {
                var count = Random.Range(_MinCount, _MaxCount+1);
                for(var i = 0; i < count; i++)
                {
                    var unit = _Units[Random.Range(0, _Units.Length)];
                    var newUnit = Instantiate(unit);
                    newUnit.transform.position = transform.position + ((Vector3)(Random.insideUnitCircle * Vector2.one));
                }
                _Time = Random.Range(_IntervalMin, _IntervalMax);
            }
        }
    }
}