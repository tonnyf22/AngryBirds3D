using AngryBirds3D.Birds.Abilities.Triplication;
using AngryBirds3D.Visuals;
using UnityEngine;

namespace AngryBirds3D.Throwables.Birds.Abilities.Triplication.Visuals
{
    public class VisualEffectsPhysicsOnly : ObjectVisuals
    {
        [SerializeField]
        private HitManagerPhysicsOnly _hitManagerPhysicsOnly;

        [SerializeField]
        private ParticleSystem _hitParticleSystem;

        void OnEnable()
        {
            _hitManagerPhysicsOnly.OnHitOccuredEvent += PlayHitEffect;
        }

        private void PlayHitEffect()
        {
            InstantiateAndPlayVisualEffect(_hitParticleSystem);
        }

        void OnDisable()
        {
            _hitManagerPhysicsOnly.OnHitOccuredEvent += PlayHitEffect;
        }
    }
}