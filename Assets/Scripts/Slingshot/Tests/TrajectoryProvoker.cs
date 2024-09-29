using UnityEngine;

namespace Slingshot.Tests
{
	public class TrajectoryProvoker : MonoBehaviour
	{
		public TrajectoryPrediction TrajectoryPrediction;

		// throwable data
		[Range(1.0f, 50.0f)]
		public float ThrowableMass;
		[Range(0.0f, 5.0f)]
		public float ThrowableDrag;
		public Transform ThrowableTransform;

		// slingshot data
		public Transform ShotTransform;
		[Range(0.0f, 100.0f)]
		public float ShotForce;

		void Update()
		{
			ShotData shotData = CreateShotData();
			TrajectoryPrediction.CreateShotTrajectory(shotData);
		}

		private ShotData CreateShotData()
		{
			return
				new ShotData(
					ThrowableMass,
					ThrowableDrag,
					ThrowableTransform.position,
					ShotTransform.forward,
					ShotForce
				);
		}
	}
}
