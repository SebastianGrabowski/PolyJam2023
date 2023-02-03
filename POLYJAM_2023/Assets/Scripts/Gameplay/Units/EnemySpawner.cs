namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]private EnemyUnit[] _Units;
        [SerializeField]private float _Interval;
        [SerializeField]private int _MinCount;
        [SerializeField]private int _MaxCount;

        private float _Time;

        void Update()
        {
            if(GameController.Instance.IsGameOver)
                return;

            _Time += Time.deltaTime;
            if(_Time >= _Interval)
            {
                _Time = 0.0f;
                var count = Random.Range(_MinCount, _MaxCount+1);
                for(var i = 0; i < count; i++)
                {
                    var unit = _Units[Random.Range(0, _Units.Length)];
                    var newUnit = Instantiate(unit);
                    newUnit.transform.position = transform.position + ((Vector3)(Random.insideUnitCircle * Vector2.one));
                }
            }
        }
    }
}