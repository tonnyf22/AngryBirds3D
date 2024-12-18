using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace AngryBirds3D.Throwables
{
    public class AbilityInput : MonoBehaviour
    {
        public event Action AbilityActivatedByInputEvent;

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
            if (IsConditionsFitInputServe())
            {
                ServeTouchInput();
            }
        }

		private bool IsConditionsFitInputServe()
		{
			return IsInputAllowed() && IsNotTouchTheUI();
		}

		private bool IsInputAllowed()
		{
			return EnhancedTouchSupport.enabled;
		}

		private bool IsNotTouchTheUI()
		{
			return !EventSystem.current.IsPointerOverGameObject();
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
                    AbilityActivatedByInputEvent?.Invoke();
                }
            }
        }
    }
}
