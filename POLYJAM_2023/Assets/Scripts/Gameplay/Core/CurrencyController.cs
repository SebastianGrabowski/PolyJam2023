namespace Gameplay
{

    using UnityEngine;
    using UnityEngine.Events;

    public class CurrencyController : MonoBehaviour
    {
        public static UnityAction OnChanged { get; set; }

        private static int _Value;
        public static int Value
        {
            get => _Value;
            set
            {
                _Value = value;
                OnChanged?.Invoke();
            }
        }

        private void Start()
        {
            Value = 20;
        }
    }
}