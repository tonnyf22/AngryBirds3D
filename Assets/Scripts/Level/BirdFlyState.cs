using UnityEngine;

namespace AngryBirds3D.Level
{
	public class BirdFlyState : MonoBehaviour
	{
        [SerializeField]
        private LevelSM _levelSM;

		[SerializeField]
		private BirdHitState _birdHitState;

		// camera tracking

		// birds ability

		void OnEnable()
		{
			EnableNecessaryFunctionality();

			SubscribeToNecessaryEvents();
		}

		private void SubscribeToNecessaryEvents()
		{
			
		}

		private void ChangeState()
		{
            _levelSM.ChangeState(this, _birdHitState);
		}

		private void EnableNecessaryFunctionality()
		{
			
		}

		void OnDisable()
		{
			DisableUsedFunctionality();

			UnsubscribeFromUsedEvents();
		}

		private void UnsubscribeFromUsedEvents()
		{
			
		}

		private void DisableUsedFunctionality()
		{
			
		}
	}
}
