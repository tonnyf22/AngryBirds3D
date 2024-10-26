using UnityEngine;
using AngryBirds3D.Visuals;
using AngryBirds3D.Birds;

namespace AngryBirds3D.Throwables.Birds.Visuals
{
    public class VisualEffects : ObjectVisuals
    {
        [SerializeField]
        private BirdAbility _birdAbility;
        [SerializeField]
        private HitManager _hitManager;

        [SerializeField]
        private ParticleSystem _abilityActivationParticleSystem;
        [SerializeField]
        private ParticleSystem _hitParticleSystem;

        void OnEnable()
        {
            _birdAbility.AbilityActivatedEvent += PlayAbilityActivationEffect;
            _hitManager.OnHitOccuredEvent += PlayHitEffect;
        }

        private void PlayAbilityActivationEffect()
        {
            InstantiateAndPlayVisualEffect(_abilityActivationParticleSystem);
        }

        private void PlayHitEffect()
        {
            InstantiateAndPlayVisualEffect(_hitParticleSystem);
        }

        void OnDisable()
        {
            _birdAbility.AbilityActivatedEvent += PlayAbilityActivationEffect;
            _hitManager.OnHitOccuredEvent += PlayHitEffect;
        }
    }
}