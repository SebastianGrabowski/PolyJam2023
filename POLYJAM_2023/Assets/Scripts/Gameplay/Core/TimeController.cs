namespace Gameplay
{

    using UnityEngine;
    using UnityEngine.Events;

    public class TimeController : MonoBehaviour
    {

        private static int _Value;
        public static int Value
        {
            get => _Value;
            set
            {
                _Value = value;
            }
        }

        private void Start()
        {
            Value = 0;
        }
    }
}