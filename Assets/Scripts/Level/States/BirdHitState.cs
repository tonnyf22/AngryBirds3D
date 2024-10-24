using AngryBirds3D.Birds;
using AngryBirds3D.GameCamera.Slingshot;
using AngryBirds3D.Slingshot;
using AngryBirds3D.Throwables;
using UnityEngine;

namespace AngryBirds3D.Level.States
{
    public class BirdHitState : MonoBehaviour
    {
        [SerializeField]
        private LevelSM _levelSM;

        [SerializeField]
        private PassCheckState _passCheckState;

        [SerializeField]
        private ThrowableContainer _throwableContainer;

		[SerializeField]
		private ThrowableTrack _throwableTrack;

        private AbilityInput _abilityInput;
        private BirdAbility _birdAbility;
        private HitManager _hitManager;

        void OnEnable()
        {
			SetUpVariables();

            SubscribeToNecessaryEvents();

            StartDelayAfterHit();
        }

        // were not disabled on previous state, so no need to enable again
		// private void EnableNecessaryFunctionality()
		// {
		// 	_abilityInput.enabled = true;
		// 	_birdAbilityInput.enabled = true;
		// 	_hitManager.enabled = true;
		// }

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
		}

		private void SubscribeToNecessaryEvents()
		{
			_hitManager.DelayFinishedEvent += ChangeLogicalState;
		}

		private void ChangeLogicalState()
		{
            _levelSM.ChangeState(this, _passCheckState);
		}

        private void StartDelayAfterHit()
        {
            _hitManager.StartDelayAfterHit();
        }

        void OnDisable()
        {
			ResetCameraPosition();

			DisableBirdAbilityActivation();

            DisableUsedFunctionality();

            UnsubscribeFromUsedEvents();
        }

		private void ResetCameraPosition()
		{
			_throwableTrack.RestoreDefaultTransform();
		}

		private void DisableBirdAbilityActivation()
		{
			_birdAbility.DisableAbilityActivation();
		}

		private void DisableUsedFunctionality()
		{
			_abilityInput.enabled = false;
			// _birdAbility.enabled = false;
			_hitManager.enabled = false;
		}

		private void UnsubscribeFromUsedEvents()
		{
			_hitManager.DelayFinishedEvent -= ChangeLogicalState;
		}
    }
}
