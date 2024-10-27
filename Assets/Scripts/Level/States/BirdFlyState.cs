using AngryBirds3D.Birds;
using AngryBirds3D.Slingshot;
using AngryBirds3D.Throwables;
using AngryBirds3D.Throwables.Birds;
using UnityEngine;

namespace AngryBirds3D.Level.States
{
	public class BirdFlyState : MonoBehaviour
	{
		[SerializeField]
		private LevelSM _levelSM;

		[SerializeField]
		private BirdHitState _birdHitState;

		[SerializeField]
		private GameCamera.Slingshot.ThrowableTrack _throwableTrack;

		[SerializeField]
		private ThrowableContainer _throwableContainer;

		private AbilityInput _abilityInput;
		private BirdAbility _birdAbility;
		private BirdLifeEndManager _hitManager;

		private ForwardLookManager _forwardLookManager;

		void OnEnable()
		{
			EnableNecessaryFunctionality();

			SubscribeToNecessaryEvents();
		}

		private void EnableNecessaryFunctionality()
		{
			_throwableTrack.enabled = true;

			SetUpVariables();

			_abilityInput.enabled = true;
			_birdAbility.enabled = true;
			_hitManager.enabled = true;

			_forwardLookManager.enabled = true;
		}

		private void SetUpVariables()
		{
			_abilityInput = 
				_throwableContainer
				.CurrentThrowable
				.GetComponent<AbilityInput>();
			_birdAbility = 
				_throwableContainer
				.CurrentThrowable
				.GetComponent<BirdAbility>();
			_hitManager = 
				_throwableContainer
				.CurrentThrowable
				.GetComponent<BirdLifeEndManager>();

			_forwardLookManager = 
				_throwableContainer
				.CurrentThrowable
				.GetComponent<ForwardLookManager>();
		}

		private void SubscribeToNecessaryEvents()
		{
			_hitManager.BirdLifeEndedEvent += ChangeLogicalState;
		}

		private void ChangeLogicalState()
		{
			_levelSM.ChangeState(this, _birdHitState);
		}

		void OnDisable()
		{
			DisableUsedFunctionality();

			UnsubscribeFromUsedEvents();
		}

		// turn of on the exiting from bird hit state
		private void DisableUsedFunctionality()
		{
			_throwableTrack.enabled = false;

		// 	_abilityInput.enabled = false;
			_birdAbility.enabled = false;
		// 	_hitManager.enabled = false;

			_forwardLookManager.enabled = false;
		}

		private void UnsubscribeFromUsedEvents()
		{
			_hitManager.BirdLifeEndedEvent -= ChangeLogicalState;
		}
	}
}
