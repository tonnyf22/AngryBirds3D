using UnityEngine;

namespace AngryBirds3D.GameCamera.FreeMode
{
	public class CameraMovement : MonoBehaviour
	{
		[SerializeField]
		private MovementInput _movementInput;

		[SerializeField]
		[Tooltip("Transform, which position will be changed by touch input. Followed by camera, using interpolation")]
		private Transform _transformToFollow;

		[SerializeField]
		[Range(1.0f, 20.0f)]
		private float _interpolationSpeed;

		[Header("Movement boundaries")]
		[SerializeField]
		private float _minX;
		[SerializeField]
		private float _maxX;
		[SerializeField]
		private float _minZ;
		[SerializeField]
		private float _maxZ;

		private Vector3 _moveToPosition;

		void OnEnable()
		{
			_moveToPosition = transform.position;

			_movementInput.FingerMovedEvent += ChangePosition;
		}

		void OnDisable()
		{
			_movementInput.FingerMovedEvent -= ChangePosition;
		}

		void Update()
		{
			if (transform.position != _moveToPosition)
			{
				transform.position = 
					Vector3.Slerp(
						transform.position, 
						_moveToPosition, 
						_interpolationSpeed * Time.deltaTime);
			}
		}

		private void ChangePosition(Vector2 rawDrag)
		{
			_moveToPosition += 
				CalculateMovementVector(rawDrag);
			
			ClampMoveToPosition();
		}

		private Vector3 CalculateMovementVector(Vector3 rawDrag)
		{
			float x = rawDrag.y;
			float z = -rawDrag.x;

			return new Vector3(x, 0.0f, z);
		}

		private void ClampMoveToPosition()
		{
			_moveToPosition = 
				new Vector3(
					Mathf.Clamp(_moveToPosition.x, _minX, _maxX),
					_moveToPosition.y,
					Mathf.Clamp(_moveToPosition.z, _minZ, _maxZ)
				);
		}
	}
}
