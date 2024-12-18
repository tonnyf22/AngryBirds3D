using AngryBirds3D.GameCamera.Slingshot;
using AngryBirds3D.Slingshot;
using AngryBirds3D.Throwables;
using AngryBirds3D.Throwables.Birds;
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
        private BirdLifeEndManager _hitManager;

        void OnEnable()
        {
			SetUpVariables();

            SubscribeToNecessaryEvents();

            StartDelayAfterHit();
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

			_hitManager.enabled = false;
		}

		private void UnsubscribeFromUsedEvents()
		{
			_hitManager.DelayFinishedEvent -= ChangeLogicalState;
		}
    }
}
