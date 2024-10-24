using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace AngryBirds3D.Slingshot
{
	public class SlingshotSpringInput : MonoBehaviour
	{
		public event Action<Vector3> ChangeThrowablePositionAndForwardDirectionEvent;
		public event Action RecalculateTrajectoryPredictionEvent;

		public event Action<float> RotateSlingshotEvent;

		public event Action FirstTensionMadeEvent;
		public event Action InitiateReleaseLogicEvent;

		[SerializeField]
		private LayerMask _touchPlaneLayer;

		[SerializeField]
		[Range(1.0f, 20.0f)]
		private float _horizontalAimSensitivity;

		private Camera _camera;

		private bool isFirstTension = true;
		
		// raycast related
		private Ray _ray;
		private RaycastHit _hit;

		private Vector3 _lastHitPoint;

		void Start()
		{
			_camera = Camera.main;
		}

		private bool _isTensionTouchTracked;
		private Touch _tensionTouch;

		private bool _isAimTouchTracked;
		private Touch _aimTouch;

		void OnEnable()
		{
			_aimTouch = new Touch();
			_tensionTouch = new Touch();
		}

		void OnDisable()
		{
		}

		void Update()
		{
			if (IsConditionsFitInputServe())
			{
				ServeTensionInput();
				ServeAimInput();
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

		private void ServeTensionInput()
		{
			if (Touch.activeTouches.Count > 0)
			{
				if (!_isTensionTouchTracked)
				{
					if (IsHitTensionPlane(Touch.activeTouches[0].screenPosition))
					{
						_isTensionTouchTracked = true;
						_tensionTouch = Touch.activeTouches[0];
					}
					else if (Touch.activeTouches.Count > 1)
					{
						if (IsHitTensionPlane(Touch.activeTouches[1].screenPosition))
						{
							_isTensionTouchTracked = true;
							_tensionTouch = Touch.activeTouches[1];
						}
					}
				}
				else
				{
					if (_tensionTouch.phase == TouchPhase.Moved || _tensionTouch.phase == TouchPhase.Stationary)
					{
                        if (isFirstTension)
                        {
                            FirstTensionMadeEvent?.Invoke();
                            isFirstTension = false;
                        }

						// thats a prediction obviously

						if (IsHitTensionPlane(_tensionTouch.screenPosition))
						{
							_lastHitPoint = _hit.point;
						}

						ChangeThrowablePositionAndForwardDirectionEvent?.Invoke(_lastHitPoint);

						RecalculateTrajectoryPredictionEvent?.Invoke();
					}
					else if (_tensionTouch.phase == TouchPhase.Ended || _tensionTouch.phase == TouchPhase.Canceled)
					{
						// thats a release obviously

						_isTensionTouchTracked = false;

						isFirstTension = true;

						InitiateReleaseLogicEvent?.Invoke();
					}
				}
			}
		}

		private bool IsHitTensionPlane(Vector2 screenTouchPosition)
		{
			_ray = 
				_camera.ScreenPointToRay(screenTouchPosition);

			return 
				Physics.Raycast(
					_ray, 
					out _hit, 
					Mathf.Infinity, 
					_touchPlaneLayer);
		}

		private void ServeAimInput()
		{
			if (Touch.activeTouches.Count > 0)
			{
				if (!_isAimTouchTracked)
				{
					if (_tensionTouch.valid)
					{
						if (Touch.activeTouches[0].touchId != _tensionTouch.touchId)
						{
							_isAimTouchTracked = true;
							_aimTouch = Touch.activeTouches[0];
						}
						else if (Touch.activeTouches.Count > 1)
						{
							if (Touch.activeTouches[1].touchId != _tensionTouch.touchId)
							{
								_isAimTouchTracked = true;
								_aimTouch = Touch.activeTouches[1];
							}
						}
					}
				}
				else
				{
					if (_aimTouch.phase == TouchPhase.Moved || _aimTouch.phase == TouchPhase.Stationary)
					{
						Vector2 dragRaw = _aimTouch.delta;
						Vector2 drag = AdjustAimDrag(dragRaw);
						float rotation = CalculateHorisontalRotationFromAimDrag(drag);

						RotateSlingshotEvent?.Invoke(rotation);
					}
					else if (_aimTouch.phase == TouchPhase.Ended || _aimTouch.phase == TouchPhase.Canceled)
					{
						_isAimTouchTracked = false;
					}
				}
			}
		}

		private Vector2 AdjustAimDrag(Vector2 drag)
		{
			return drag * _horizontalAimSensitivity / 100.0f;
		}

		private float CalculateHorisontalRotationFromAimDrag(Vector2 drag)
		{
			return drag.x;
		}
	}
}
