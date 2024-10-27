using System;
using AngryBirds3D.Throwables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AngryBirds3D.Birds.Abilities.Triplication
{
	[RequireComponent(typeof(SphereCollider))]
	public class Triplication : BirdAbility
	{
		[SerializeField]
		private GameObject _birdPrefabPhysicsOnly;

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
			_randomlyRotatedRightVector;

		void Start()
		{
			_rb = GetComponent<Rigidbody>();
			_sc = GetComponent<SphereCollider>();
		}

		protected override void AbilityActivatedByInput()
		{
			_currentVelocity = _rb.velocity.magnitude;

			CalculatePositionsAndDirections();
			InstantiateAdditionalBirdsAndApplyImpulseForce();
		}

		private void CalculatePositionsAndDirections()
		{
			(_randomlyRotatedUpVector, _randomlyRotatedRightVector) =
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
			Vector3 additionalBirdOnePosition;
			Vector3 additionalBirdTwoPosition;

			float ratio = 
				_sc.radius * 
				transform.localScale.x * 
				2.5f;
			Vector3 scaled = 
				ScaleVectorByRatio(
					_randomlyRotatedUpVector, 
					ratio);
			additionalBirdOnePosition =
				ShiftOfCurrentPositionBy(scaled);
			additionalBirdTwoPosition =
				ShiftOfCurrentPositionBy(-scaled);

			return (additionalBirdOnePosition, additionalBirdTwoPosition);
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
					-_angleDeviation, 
					_randomlyRotatedRightVector) * 
				transform.forward;
			Vector3 birdTwoDirection =
				Quaternion.AngleAxis(
					_angleDeviation, 
					_randomlyRotatedRightVector) * 
				transform.forward;

			return (birdOneDirection, birdTwoDirection);
		}

		private void InstantiateAdditionalBirdsAndApplyImpulseForce()
		{
			GameObject bird1 = 
				Instantiate(
					_birdPrefabPhysicsOnly, 
					_birdOnePosition, 
					Quaternion.identity);
			bird1.transform.forward = _birdOneDirection;
			Rigidbody bird1RB = bird1.GetComponent<Rigidbody>();
			bird1RB.AddForce(
				bird1RB.mass * _currentVelocity * _birdOneDirection,
				ForceMode.Impulse);
			bird1RB.GetComponent<ForwardLookManager>().enabled = true;

			GameObject bird2 = 
				Instantiate(
					_birdPrefabPhysicsOnly, 
					_birdTwoPosition, 
					Quaternion.identity);
			bird2.transform.forward = _birdTwoDirection;
			Rigidbody bird2RB = bird2.GetComponent<Rigidbody>();
			bird2RB.AddForce(
				bird2RB.mass * _currentVelocity * _birdTwoDirection,
				ForceMode.Impulse);
			bird2RB.GetComponent<ForwardLookManager>().enabled = true;
		}
	}
}
