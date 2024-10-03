using System;
using AngryBirds3D.Managers;
using UnityEngine;

namespace AngryBirds3D.Slingshot
{
	public class SpringTension : MonoBehaviour
	{
		[SerializeField]
		private ThrowableContainer _throwableContainerScript;

		[SerializeField]
		private SlingshotSpringInput _slingshotSpringInput;

		[SerializeField]
		private Transform _shotPointActual;
		private Vector3 _shotPointActualInitPosition;
		[SerializeField]
		private Transform _shotPointReference;

		[SerializeField]
		[Range(30.0f, 100.0f)]
		private float _tensionForcePerDistanceUnit;

		private TrajectoryPrediction _trajectoryPrediction;

		private float _stretchDistanceThrowable = 0.0f;
		private Vector3 _forceDirectionThrowable = Vector3.forward;
		private Rigidbody _rigidBodyThrowable = null;

		void Start()
		{
			_shotPointActualInitPosition = _shotPointActual.position;

			_trajectoryPrediction = GetComponent<TrajectoryPrediction>();
		}

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
				if (_throwableContainerScript.CurrentThrowable == null)
				{
					return;
				}
				else
				{
					_rigidBodyThrowable = GetThrowableRigidBody();
				}
			}

			_stretchDistanceThrowable = CalculateDistanceToShotReference();
			_forceDirectionThrowable = CalculateAppliedForceDirection();
			float appliedForce = 
				_stretchDistanceThrowable * 
				_tensionForcePerDistanceUnit;

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
				_throwableContainerScript
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

		private void InitiateReleaseLogic()
		{
			ReleaseCurrentThrowable();

			RestoreShotPointActualInitPosition();

			// separate calls, but together for now

			_throwableContainerScript.SetupNewThrowableToShoot();
		}

		private void ReleaseCurrentThrowable()
		{
			ActivatePhysicsOnThrowable();

			_throwableContainerScript.FreeThrowable();

			ApplyImpulseToThrowable(
				_rigidBodyThrowable, 
				_forceDirectionThrowable, 
				_stretchDistanceThrowable);

			_throwableContainerScript.ForgetThrowable();

			_trajectoryPrediction.HideUnusedDotsStartingFrom(-1);
			_trajectoryPrediction.HideHitMark();
			ForgetThrowableRigidbody();
		}

		private void ActivatePhysicsOnThrowable()
		{
			SphereCollider sc = 
				_throwableContainerScript
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

		private void ForgetThrowableRigidbody()
		{
			_rigidBodyThrowable = null;
		}

		private void RestoreShotPointActualInitPosition()
		{
			_shotPointActual.position = _shotPointActualInitPosition;
		}
	}
}
