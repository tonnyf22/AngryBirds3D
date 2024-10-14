using AngryBirds3D.Destroyables.Pigs;
using AngryBirds3D.Slingshot;
using UnityEngine;

namespace AngryBirds3D.Level.States
{
	public class BirdLoadState : MonoBehaviour
	{
		[SerializeField]
		private LevelSM _levelSM;

		[SerializeField]
		private SlingshotAimState _slingshotAimState;

		[SerializeField]
		private CompletionState _completionState;

		[SerializeField]
		private ThrowableContainer _throwableContainer;

		[SerializeField]
		private PigsTrack _pigsTrack;

		void OnEnable()
		{
			SubscribeToNecessaryEvents();

			SetUpNewThrowable();
			
			ChangeLogicalState();
		}

		private void SubscribeToNecessaryEvents()
		{
			_pigsTrack.AllPigsDefeatedEvent += ChangeStateToCompletion;
		}

		private void ChangeStateToCompletion()
		{
			_levelSM.ChangeState(this, _completionState);
		}

		private void SetUpNewThrowable()
		{
			_throwableContainer.SetupNewThrowableToShoot();
		}

		private void ChangeLogicalState()
		{
			_levelSM.ChangeState(this, _slingshotAimState);
		}

		void OnDisable()
		{
			UnsubscribeFromNecessaryEvents();
		}

		private void UnsubscribeFromNecessaryEvents()
		{
			_pigsTrack.AllPigsDefeatedEvent -= ChangeStateToCompletion;
		}
	}
}
