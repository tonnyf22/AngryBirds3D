using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AngryBirds3D.Throwables
{
    public class AbilityInput : MonoBehaviour
    {
        public event Action AbilityActivatedEvent;

        [SerializeField]
        private InputAction _touchInputAction;

        void OnEnable()
        {
            _touchInputAction.Enable();
        }

        void OnDisable()
        {
            _touchInputAction.Disable();
        }

        void Update()
        {
            ServeTouchInput();
        }

        private void ServeTouchInput()
        {
            if (_touchInputAction.triggered)
            {
                _touchInputAction.Disable();
                AbilityActivatedEvent?.Invoke();
            }
        }
    }
}
