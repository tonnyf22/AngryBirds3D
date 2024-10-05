using System;
using AngryBirds3D.Managers;
using UnityEngine;

namespace AngryBirds3D.Slingshot
{
	public class SpringTension : MonoBehaviour
	{
		[SerializeField]
		private SlingshotSpringInput _slingshotSpringInput;

		[SerializeField]
		private TrajectoryPrediction _trajectoryPrediction;

		[SerializeField]
		private ThrowableContainer _throwableContainer;

		[SerializeField]
		private Transform _shotPointActual;
		[SerializeField]
		private Transform _shotPointReference;

		[SerializeField]
		[Range(30.0f, 100.0f)]
		private float _tensionForcePerDistanceUnit;

		private float _stretchDistanceThrowable = 0.0f;
		private Vector3 _forceDirectionThrowable = Vector3.forward;
		private Rigidbody _rigidBodyThrowable = null;

		void OnEnable()
		{
			_slingshotSpringInput
				.ChangeThrowablePositionEvent += ChangeThrowablePosition;
			_slingshotSpringInput
				.RecalculateTrajectoryPredictionEvent += RecalculateTrajectoryPrediction;
			_slingshotSpringInput
				.InitiateReleaseLogicEvent += InitiateReleaseLogic;
		}

		void OnDisable()
		{
			_slingshotSpringInput
				.ChangeThrowablePositionEvent -= ChangeThrowablePosition;
			_slingshotSpringInput
				.RecalculateTrajectoryPredictionEvent -= RecalculateTrajectoryPrediction;
			_slingshotSpringInput
				.InitiateReleaseLogicEvent -= InitiateReleaseLogic;
		}

		private void ChangeThrowablePosition(Vector3 position)
		{
		   _shotPointActual.position = position;
		}

		private void RecalculateTrajectoryPrediction()
		{
			if (_rigidBodyThrowable == null)
			{
                _rigidBodyThrowable = GetThrowableRigidBody();
			}

			_stretchDistanceThrowable = CalculateDistanceToShotReference();
			_forceDirectionThrowable = CalculateAppliedForceDirection();
			float appliedForce = CalculateAppliedForce();

			ShotData shotData = 
				new ShotData(
					_rigidBodyThrowable.mass, 
					_rigidBodyThrowable.drag, 
					_shotPointActual.position, 
					_forceDirectionThrowable, 
					appliedForce
				);
			_trajectoryPrediction.CreateShotTrajectory(shotData);
		}

		private Rigidbody GetThrowableRigidBody()
		{
			return 
				_throwableContainer
				.CurrentThrowable
				.GetComponent<Rigidbody>();
		}

		private float CalculateDistanceToShotReference()
		{
			float distance = 
				Vector3.Distance(
					_shotPointReference.position,
					_shotPointActual.position
				);

			return distance;
		}

		private Vector3 CalculateAppliedForceDirection()
		{
			Vector3 direction =
				(_shotPointReference.position - _shotPointActual.position)
				.normalized;

			return direction;
		}

		private float CalculateAppliedForce()
		{
			return 
				_stretchDistanceThrowable * 
				_tensionForcePerDistanceUnit;
		}

		private void InitiateReleaseLogic()
		{
			ReleaseCurrentThrowableAndAddImpulse();

			FreeAndForgetThrowable();

			CleanUpAfterShot();

			// separate calls, but together for now

			_throwableContainer.SetupNewThrowableToShoot();
		}

		private void FreeAndForgetThrowable()
		{
			_throwableContainer.FreeThrowable();
			_throwableContainer.ForgetThrowable();
		}

		private void ReleaseCurrentThrowableAndAddImpulse()
		{
			ActivatePhysicsOnThrowable();
			ApplyImpulseToThrowable(
				_rigidBodyThrowable, 
				_forceDirectionThrowable, 
				_stretchDistanceThrowable);
		}

		private void ActivatePhysicsOnThrowable()
		{
			SphereCollider sc = 
				_throwableContainer
				.CurrentThrowable
				.GetComponent<SphereCollider>() ;
			sc.enabled = true;

			Rigidbody rb = GetThrowableRigidBody();
			rb.isKinematic = false;
		}

		private void ApplyImpulseToThrowable(
			Rigidbody rigidbody,
			Vector3 forceDirection,
			float stretchDistance)
		{
			rigidbody.AddForce(
				_tensionForcePerDistanceUnit * stretchDistance *
				forceDirection,
				ForceMode.Impulse);
		}

		private void CleanUpAfterShot()
		{
			_trajectoryPrediction.HideUnusedDotsStartingAfter(-1);
			_trajectoryPrediction.HideHitMark();
			ForgetThrowableRigidbody();
			RestoreShotPointActualInitPosition();
		}

		private void ForgetThrowableRigidbody()
		{
			_rigidBodyThrowable = null;
		}

		private void RestoreShotPointActualInitPosition()
		{
			_shotPointActual.position = _shotPointReference.position;
		}
	}
}
