namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]private GameObject _Pointer;
        [SerializeField]private Camera _Cam;

        private PlayerUnit _ActiveUnit;

        private Plane _Plane;

        private bool _Active;
        public bool Active
        {
            get => _Active;
            set
            {
                if(_Active != value)
                {
                    _Active = value;
                    _Pointer.SetActive(_Active);
                }
            }
        }

        private void Start()
        {
            _Plane = new Plane(Vector3.forward, 0.0f);
        }

        public void SetUnit(PlayerUnit unit)
        {
            _ActiveUnit = unit;
            Active = _ActiveUnit != null;
        }

        // Update is called once per frame
        void Update()
        {
            _Pointer.gameObject.SetActive(Active);

            if (Active)
            {
                
                Vector2 pos = Vector2.zero;
                var ray = _Cam.ScreenPointToRay(Input.mousePosition);   
                if (_Plane.Raycast(ray, out var distance))
                {
                    pos = ray.GetPoint(distance);
                } else
                {
                    return;
                }

                _Pointer.transform.position = pos;


                if(Input.GetMouseButtonDown(0))
                {
                    //spawn
                    if (_ActiveUnit.Cost <= CurrencyController.Value)
                    {
                        CurrencyController.Value -= _ActiveUnit.Cost;
                        var newUnit = Instantiate(_ActiveUnit);
                        newUnit.transform.position = pos;
                    }
                }
                if(Input.GetMouseButtonDown(1))
                {
                    SetUnit(null);
                }
            }
        }
    }
}