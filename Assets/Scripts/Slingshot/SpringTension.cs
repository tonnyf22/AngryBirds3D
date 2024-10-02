using System;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Slingshot
{
    public class SpringTension : MonoBehaviour
    {
        [SerializeField]
        private InputAction _touchInputAction;

        [SerializeField]
        private ThrowableManager _throwableContainerScript;

        [SerializeField]
        private LayerMask _touchPlaneLayer;

        private Camera _camera;

        // raycast related
        private Vector2 _touchPosition;
        private Ray _ray;
        private RaycastHit _hit;

        // touch event
        private event Action ServeTouchInputEvent;

        // input coeffs
        private bool _isHoldingStretch = false;

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

            ServeTouchInputEvent += ServeTouchInput;
        }

        void Update()
        {
            ServeTouchInputEvent?.Invoke();
        }

        private void ServeTouchInput()
        {
            if (_touchInputAction.triggered)
            {
                RaycastFromTouchPosition();
            }
            else
            {
                CheckInputRelease();
            }
        }

        private void RaycastFromTouchPosition()
        {
            _touchPosition = _touchInputAction.ReadValue<Vector2>();

            if (IsHitTouchPlane())
            {
                ChangeHoldingStretchState(true);

                ChangeThrowablePosition();
            }
        }

        private bool IsHitTouchPlane()
        {
            _ray = _camera.ScreenPointToRay(_touchPosition);

            return Physics.Raycast(_ray, out _hit, _touchPlaneLayer);
        }

        private void ChangeHoldingStretchState(bool isHolding)
        {
            _isHoldingStretch = isHolding;

            if (!isHolding)
            {
                InitiateReleaseLogic();
            }
        }

        private void ChangeThrowablePosition()
        {
           _throwableContainerScript.ShotPointActual.position = _hit.point;
        }

        private void CheckInputRelease()
        {
            if (Touch.activeTouches.Count > 0)
            {
                Touch touch = Touch.activeTouches[0];

                if (touch.phase == TouchPhase.Ended ||
                    touch.phase == TouchPhase.Canceled)
                {
                    ChangeHoldingStretchState(false);
                }
            }
        }

        private void InitiateReleaseLogic()
        {
            _throwableContainerScript.ReleaseCurrentThrowable();

            _throwableContainerScript.SetupNewThrowableToShoot();

            CheckCurrentThrowableToMaintainServiceOfTouchInput();
        }

        private void CheckCurrentThrowableToMaintainServiceOfTouchInput()
        {
            if (_throwableContainerScript.CurrentThrowable == null)
            {
                ServeTouchInputEvent -= ServeTouchInput;
            }
        }
    }
}
