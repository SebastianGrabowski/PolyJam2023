namespace Gameplay.Units
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]private CursorScript _Cursor;
        [SerializeField]private SpriteRenderer _Pointer;
        [SerializeField]private Camera _Cam;
        [SerializeField]private Color _ValidColor;
        [SerializeField]private Color _InvalidColor;

        private PlayerUnit _ActiveUnit;

        public static bool LockByHoverGod;

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
                    _Cursor.SetCursor(_Active);
                }
            }
        }

        private void Start()
        {
            _Plane = new Plane(Vector3.forward, 0.0f);
            CurrencyController.OnChanged += SetCursorAvailaity;
        }

        public Unit ActiveUnit => _ActiveUnit;

        public void SetUnit(PlayerUnit unit)
        {
            _ActiveUnit = unit;
            Active = _ActiveUnit != null;
        }

        private void SetCursorAvailaity()
        {
            if(_ActiveUnit == null) return;

            var canPlace = _ActiveUnit.Cost <= CurrencyController.Value;
            _Cursor.SetCursorOnMapAvailabity(canPlace);
        }

        // Update is called once per frame
        void Update()
        {
            if(GameController.Instance.IsGameOver)
                return;

            _Pointer.gameObject.SetActive(Active);

            if (Active)
            {
                if(LockByHoverGod || UIController.Instance.IsOverUI) _Cursor.SetCursor(false);
                else _Cursor.SetCursor(true);


                
                var validDistance = false;
                Vector2 pos = Vector2.zero;
                var ray = _Cam.ScreenPointToRay(Input.mousePosition);   
                if (_Plane.Raycast(ray, out var distance))
                {
                    pos = ray.GetPoint(distance);
                    var d = Vector2.Distance(Vector2.zero, pos);
                    validDistance = true;// d > 4.0f;
                    //_Pointer.color = validDistance && !LockByHoverGod ? _ValidColor : _InvalidColor;
                } else
                {
                    return;
                }

                _Pointer.transform.position = pos;


                if(Input.GetMouseButtonDown(0) && validDistance && !LockByHoverGod && !UIController.Instance.IsOverUI)
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

        private void OnDestroy()
        {
            CurrencyController.OnChanged -= SetCursorAvailaity;
        }
    }
}