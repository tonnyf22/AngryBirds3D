using AngryBirds3D.Birds;
using AngryBirds3D.Slingshot;
using AngryBirds3D.Throwables;
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

		[SerializeField]
		private ThrowableContainer _throwableContainer;

		private AbilityInput _abilityInput;
		private BirdAbility _birdAbility;
		private HitManager _hitManager;

		private ForwardLookManager _forwardLookManager;

		void OnEnable()
		{
			EnableNecessaryFunctionality();

			SubscribeToNecessaryEvents();
		}

		private void EnableNecessaryFunctionality()
		{
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
				.GetComponent<HitManager>();

			_forwardLookManager = 
				_throwableContainer
				.CurrentThrowable
				.GetComponent<ForwardLookManager>();
		}

		private void SubscribeToNecessaryEvents()
		{
			_hitManager.OnHitOccuredEvent += ChangeState;
		}

		private void ChangeState()
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
		// 	_abilityInput.enabled = false;
			_birdAbility.enabled = false;
		// 	_hitManager.enabled = false;

			_forwardLookManager.enabled = false;
		}

		private void UnsubscribeFromUsedEvents()
		{
			_hitManager.OnHitOccuredEvent -= ChangeState;
		}
	}
}
