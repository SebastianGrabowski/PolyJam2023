namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]private SpriteRenderer _Pointer;
        [SerializeField]private Camera _Cam;
        [SerializeField]private Color _ValidColor;
        [SerializeField]private Color _InvalidColor;

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
                    _Pointer.gameObject.SetActive(_Active);
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
            if(GameController.Instance.IsGameOver)
                return;

            _Pointer.gameObject.SetActive(Active);

            if (Active)
            {
                
                var validDistance = false;
                Vector2 pos = Vector2.zero;
                var ray = _Cam.ScreenPointToRay(Input.mousePosition);   
                if (_Plane.Raycast(ray, out var distance))
                {
                    pos = ray.GetPoint(distance);
                    var d = Vector2.Distance(Vector2.zero, pos);
                    validDistance = d > 4.0f;
                    _Pointer.color = validDistance ? _ValidColor : _InvalidColor;
                } else
                {
                    return;
                }

                _Pointer.transform.position = pos;


                if(Input.GetMouseButtonDown(0) && validDistance)
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