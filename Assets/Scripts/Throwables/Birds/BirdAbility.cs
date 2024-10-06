using AngryBirds3D.Throwables;
using UnityEngine;

namespace AngryBirds3D.Birds
{
    public abstract class BirdAbility : MonoBehaviour
    {
        [SerializeField]
        private AbilityInput _abilityInput;

        void OnEnable()
        {
            _abilityInput.AbilityActivatedEvent += AbilityActivated;
        }

        void OnDisable()
        {
            _abilityInput.AbilityActivatedEvent -= AbilityActivated;
        }

        protected abstract void AbilityActivated();
    }
}
