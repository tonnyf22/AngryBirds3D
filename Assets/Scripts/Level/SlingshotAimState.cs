using AngryBirds3D.Slingshot;
using UnityEngine;

namespace AngryBirds3D.Level
{
	public class SlingshotAimState : MonoBehaviour
	{
		[SerializeField]
		private LevelSM _levelSM;

		[SerializeField]
		private BirdFlyState _birdFlyState;

		[SerializeField]
		private SpringTension _springTension;
		[SerializeField]
		private SlingshotSpringInput _slingshotSpringInput;

		void OnEnable()
		{
			EnableNecessaryFunctionality();

			SubscribeToNecessaryEvents();
		}

		private void EnableNecessaryFunctionality()
		{
			_springTension.enabled = true;
			_slingshotSpringInput.enabled = true;
		}

		private void SubscribeToNecessaryEvents()
		{
			_slingshotSpringInput.InitiateReleaseLogicEvent += ChangeState;
		}

		private void ChangeState()
		{
            _levelSM.ChangeState(this, _birdFlyState);
		}

		void OnDisable()
		{
			DisableUsedFunctionality();

			UnsubscribeFromUsedEvents();
		}

		private void DisableUsedFunctionality()
		{
			_springTension.enabled = false;
			_slingshotSpringInput.enabled = false;
		}

		private void UnsubscribeFromUsedEvents()
		{
			_slingshotSpringInput.InitiateReleaseLogicEvent -= ChangeState;
		}
	}
}
