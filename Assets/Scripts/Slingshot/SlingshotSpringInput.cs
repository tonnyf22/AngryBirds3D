using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace AngryBirds3D.Slingshot
{
    public class SlingshotSpringInput : MonoBehaviour
    {
        public event Action<Vector3> ChangeThrowablePositionAndForwardDirectionEvent;
        public event Action RecalculateTrajectoryPredictionEvent;

        public event Action InitiateReleaseLogicEvent;

        [SerializeField]
        private InputAction _touchInputAction;

        [SerializeField]
        private LayerMask _touchPlaneLayer;

        private Camera _camera;

        // raycast related
        private Vector2 _touchPosition;
        private Ray _ray;
        private RaycastHit _hit;

        void OnEnable()
        {
            _touchInputAction.Enable();
        }

        void OnDisable()
        {
            _touchInputAction.Disable();
        }

        void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            ServeTouchInput();
        }

        private void ServeTouchInput()
        {
            if (_touchInputAction.triggered)
            {
                // remember the touch on tension plane when it starts and wait 'till it ends
                RaycastFromTouchPosition();
            }

            CheckInputRelease();
        }

        private void RaycastFromTouchPosition()
        {
            _touchPosition = _touchInputAction.ReadValue<Vector2>();

            if (IsHitTouchPlane())
            {
                ChangeThrowablePositionAndForwardDirectionEvent?.Invoke(_hit.point);

                RecalculateTrajectoryPredictionEvent?.Invoke();
            }
        }

        private bool IsHitTouchPlane()
        {
            _ray = _camera.ScreenPointToRay(_touchPosition);

            return Physics.Raycast(_ray, out _hit, Mathf.Infinity, _touchPlaneLayer);
        }

        private void CheckInputRelease()
        {
            if (Touch.activeTouches.Count > 0)
            {
                Touch touch = Touch.activeTouches[0];

                if (touch.phase == TouchPhase.Ended)
                {
                    InitiateReleaseLogicEvent?.Invoke();
                }
            }
        }
    }
}
