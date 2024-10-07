using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

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
                CheckTouchInput();
            }
        }

        private void CheckTouchInput()
        {
            if (Touch.activeTouches.Count > 0)
            {
                Touch touch = Touch.activeTouches[0];

                if (touch.phase == TouchPhase.Began)
                {
                    _touchInputAction.Disable();
                    AbilityActivatedEvent?.Invoke();
                }
            }
        }
    }
}
