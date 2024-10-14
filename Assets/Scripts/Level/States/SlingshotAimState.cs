using AngryBirds3D.Destroyables.Pigs;
using AngryBirds3D.Slingshot;
using UnityEngine;

namespace AngryBirds3D.Level.States
{
	public class SlingshotAimState : MonoBehaviour
	{
		[SerializeField]
		private LevelSM _levelSM;

		[SerializeField]
		private BirdFlyState _birdFlyState;

		[SerializeField]
		private CompletionState _completionState;

		[SerializeField]
		private Throwables.Birds.BirdsTrack _birdsTrack;

		[SerializeField]
		private SpringTension _springTension;
		[SerializeField]
		private SlingshotSpringInput _slingshotSpringInput;

		[SerializeField]
		private PigsTrack _pigsTrack;

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
			_slingshotSpringInput.InitiateReleaseLogicEvent += ChangeLogicalState;
			_pigsTrack.AllPigsDefeatedEvent += ChangeStateToCompletion;
		}

		private void ChangeStateToCompletion()
		{
			_levelSM.ChangeState(this, _completionState);
		}

		private void ChangeLogicalState()
		{
            _levelSM.ChangeState(this, _birdFlyState);
		}

		void OnDisable()
		{
			ForgetLoadedBird();

			DisableUsedFunctionality();

			UnsubscribeFromUsedEvents();
		}

		private void ForgetLoadedBird()
		{
			_birdsTrack.ForgetLastExtractedBird();
		}

		private void DisableUsedFunctionality()
		{
			_springTension.enabled = false;
			_slingshotSpringInput.enabled = false;
		}

		private void UnsubscribeFromUsedEvents()
		{
			_slingshotSpringInput.InitiateReleaseLogicEvent -= ChangeLogicalState;

			_pigsTrack.AllPigsDefeatedEvent -= ChangeStateToCompletion;
		}
	}
}
