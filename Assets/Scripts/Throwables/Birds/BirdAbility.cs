using System;
using AngryBirds3D.Throwables;
using UnityEngine;

namespace AngryBirds3D.Throwables.Birds
{
    public abstract class BirdAbility : MonoBehaviour
    {
        public event Action AbilityActivatedEvent;

        [SerializeField]
        [Range(1, 50000)]
        private int _pointsForSave;
        public int PointsForSave
        {
            get { return _pointsForSave; }
        }

        [SerializeField]
        private AbilityInput _abilityInput;

        [SerializeField]
        private bool _isAllowedActivationAfterHit;

        private bool _isUnsubscribedFromAbilityActivation;

        void OnEnable()
        {
            _abilityInput.AbilityActivatedByInputEvent += AbilityActivatedByInput;
            _abilityInput.AbilityActivatedByInputEvent += AbilityActivated;
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
                _abilityInput.AbilityActivatedByInputEvent -= AbilityActivatedByInput;
                _abilityInput.AbilityActivatedByInputEvent -= AbilityActivated;
                _isUnsubscribedFromAbilityActivation = true;
            }
        }

        private bool IsNotUnsubscribedFromAbilityActivation()
        {
            return !_isUnsubscribedFromAbilityActivation;
        }

        protected abstract void AbilityActivatedByInput();

        private void AbilityActivated()
        {
            AbilityActivatedEvent?.Invoke();
        }
    }
}
