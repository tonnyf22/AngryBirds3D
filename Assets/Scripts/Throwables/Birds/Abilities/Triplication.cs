using UnityEngine;

namespace AngryBirds3D.Birds.Abilities
{
	[RequireComponent(typeof(SphereCollider))]
	public class Triplication : BirdAbility
	{
		[SerializeField]
		private GameObject birdPrefabPhysicsOnly;

		[SerializeField]
		[Range(10.0f, 45.0f)]
		private float _angleDeviation;

		private Rigidbody _rb;
		private SphereCollider _sc;

		private float _currentVelocity;

		private Vector3 _birdOnePosition, _birdTwoPosition;
		private Vector3 _birdOneDirection, _birdTwoDirection;
		
		private Vector3 
			_randomlyRotatedUpVector, 
			_randomplyRotatedRightVector;

		void Start()
		{
			_rb = GetComponent<Rigidbody>();
			_sc = GetComponent<SphereCollider>();

			_currentVelocity = _rb.velocity.magnitude;
		}

		protected override void AbilityActivated()
		{
			CalculatePositionsAndDirections();
			InstantiateAdditionalBirdsAndApplyImpulseForce();
		}

		private void CalculatePositionsAndDirections()
		{
			CalculatePositionsAndDirections();
			(_randomlyRotatedUpVector, _randomplyRotatedRightVector) =
				GenerateRandomlyRotatedUpAndRightVector();

			(_birdOnePosition, _birdTwoPosition) =
				GenerateAdditionalBirdsPositions();

			(_birdOneDirection, _birdTwoDirection) =
				GenerateAdditionalBirdsDirections();
		}

		private (Vector3, Vector3) GenerateRandomlyRotatedUpAndRightVector()
		{
			float randomRotationDegrees = Random.value * 180.0f;
			Vector3 upRotated = 
				Quaternion.AngleAxis(
					randomRotationDegrees, 
					transform.forward) *
				transform.up;
			Vector3 rightRotated = 
				Quaternion.AngleAxis(
					randomRotationDegrees,
					transform.forward) *
				transform.right;

			return (upRotated, rightRotated);
		}

		private (Vector3, Vector3) GenerateAdditionalBirdsPositions()
		{
			// gradually get modification 'till the final position
			Vector3 additionalBirdOnePosition;

			additionalBirdOnePosition = 
				ScaleVectorByRatio(
					_randomlyRotatedUpVector, 
					_sc.radius * 1.5f);
			additionalBirdOnePosition =
				ShiftOfCurrentPositionBy(additionalBirdOnePosition);

			return (additionalBirdOnePosition, -additionalBirdOnePosition);
		}

		private Vector3 ScaleVectorByRatio(Vector3 vector, float ratio)
		{
			return vector * ratio;
		}

		private Vector3 ShiftOfCurrentPositionBy(Vector3 shift)
		{
			return transform.position + shift;
		}

		private (Vector3, Vector3) GenerateAdditionalBirdsDirections()
		{
			Vector3 birdOneDirection =
				Quaternion.AngleAxis(
					_angleDeviation, 
					_randomplyRotatedRightVector) * 
				transform.forward;
			Vector3 birdTwoDirection =
				Quaternion.AngleAxis(
					-_angleDeviation, 
					_randomplyRotatedRightVector) * 
				transform.forward;

			return (birdOneDirection, birdTwoDirection);
		}

		private void InstantiateAdditionalBirdsAndApplyImpulseForce()
		{
			GameObject bird1 = 
				Instantiate(
					birdPrefabPhysicsOnly, 
					_birdOnePosition, 
					Quaternion.identity);
			bird1.transform.forward = _birdOneDirection;
			Rigidbody bird1RB = bird1.GetComponent<Rigidbody>();
			bird1RB.AddForce(
				bird1RB.mass * _currentVelocity * _birdOneDirection,
				ForceMode.Impulse);

			GameObject bird2 = 
				Instantiate(
					birdPrefabPhysicsOnly, 
					_birdTwoPosition, 
					Quaternion.identity);
			bird2.transform.forward = _birdTwoDirection;
			Rigidbody bird2RB = bird2.GetComponent<Rigidbody>();
			bird2RB.AddForce(
				bird2RB.mass * _currentVelocity * _birdTwoDirection,
				ForceMode.Impulse);
		}
	}
}
