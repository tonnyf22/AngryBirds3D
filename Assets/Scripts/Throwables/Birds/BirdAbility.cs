using AngryBirds3D.Throwables;
using UnityEngine;

namespace AngryBirds3D.Birds
{
    public abstract class BirdAbility : MonoBehaviour
    {
        [SerializeField]
        private AbilityInput _abilityInput;

        [SerializeField]
        private bool _isAllowedActivationAfterHit;

        private bool _isUnsubscribedFromAbilityActivation;

        void OnEnable()
        {
            _abilityInput.AbilityActivatedEvent += AbilityActivated;
        }

        void OnDisable()
        {
            if (IsNotAllowedActivationAfterHit())
            {
                DisableAbilityActivation();
            }
        }

        private bool IsNotAllowedActivationAfterHit()
        {
            return !_isAllowedActivationAfterHit;
        }

        public void DisableAbilityActivation()
        {
            if (IsNotUnsubscribedFromAbilityActivation())
            {
                _abilityInput.AbilityActivatedEvent -= AbilityActivated;
                _isUnsubscribedFromAbilityActivation = true;
            }
        }

        private bool IsNotUnsubscribedFromAbilityActivation()
        {
            return !_isUnsubscribedFromAbilityActivation;
        }

        protected abstract void AbilityActivated();
    }
}
