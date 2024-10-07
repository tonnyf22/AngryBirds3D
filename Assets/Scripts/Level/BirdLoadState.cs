using AngryBirds3D.Slingshot;
using UnityEngine;

namespace AngryBirds3D.Level
{
	public class BirdLoadState : MonoBehaviour
	{
		[SerializeField]
		private LevelSM _levelSM;

		[SerializeField]
		private SlingshotAimState _slingshotAimState;

		[SerializeField]
		private ThrowableContainer _throwableContainer;

		void OnEnable()
		{
			// enable necessary scripts on objects
			// don't really need any specific "per-state" functionality

			SetUpNewThrowable();
			
			ChangeState();
		}

		private void SetUpNewThrowable()
		{
			_throwableContainer.SetupNewThrowableToShoot();
			Debug.Log(_throwableContainer.CurrentThrowable == null);
		}

		private void ChangeState()
		{
			_levelSM.ChangeState(this, _slingshotAimState);
		}

		void OnDisable()
		{
			// no specific functionality was enabled, so no need to disable anything
		}
	}
}
