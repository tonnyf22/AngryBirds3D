using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AngryBirds3D.GameCamera.FreeMode
{
    public class MovementInput : MonoBehaviour
    {
        public event Action<Vector2> FingerMovedEvent;

		[SerializeField]
		[Range(1.0f, 20.0f)]
		private float _dragSensitivity;

        [SerializeField]
        private InputAction _inputAction;

        void OnEnable()
        {
            _inputAction.Enable();
            _inputAction.performed += OnDragInput;
        }

        private void OnDragInput(InputAction.CallbackContext context)
        {
            Vector2 drag = context.ReadValue<Vector2>();

            drag = AdjustDrag(drag);

            FingerMovedEvent?.Invoke(drag);
        }

        private Vector2 AdjustDrag(Vector2 drag)
        {
            return drag * _dragSensitivity / 1000.0f;
        }

        void OnDisable()
        {
            _inputAction.performed -= OnDragInput;
            _inputAction.Enable();
        }
    }
}
