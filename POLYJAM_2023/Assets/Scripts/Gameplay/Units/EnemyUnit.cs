namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyUnit : Unit
    {


        void Update()
        {
            var middleDist = Vector2.Distance(transform.position, Vector2.zero);
            var nearestEnemy = GetNearestUnit(middleDist);
            var dir = (Vector2.zero - (Vector2)transform.position).normalized;
            if(nearestEnemy != null)
            {
                dir = ((Vector2)(nearestEnemy.transform.position - transform.position)).normalized;
            }
            _Rigidbody.velocity = dir * MoveSpeed;
        }
        protected Unit GetNearestUnit(float maxDistance)
        {
            var d = float.MaxValue;
            Unit result = null;
            var pos = transform.position;
            for(var i = 0; i < AllUnits.Count; i++)
            {
                    Debug.Log("TEST X" );
                if(AllUnits[i] is PlayerUnit)
                {
                    var dd = Vector2.Distance(AllUnits[i].transform.position, pos);
                    Debug.Log("TEST " + dd.ToString() + " " + d.ToString());
                    if(dd < d)
                    {
                        d = dd;
                        result = AllUnits[i];
                    }
                }
            }

            if(d < maxDistance)
            {
                return result;
            }
            return null;
        }
    }
}