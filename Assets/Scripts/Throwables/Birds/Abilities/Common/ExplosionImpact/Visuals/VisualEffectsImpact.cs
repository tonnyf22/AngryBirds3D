using AngryBirds3D.Visuals;
using UnityEngine;

namespace AngryBirds3D.Throwables.Birds.Abilities.Explosion.Visuals
{
    public class VisualEffectsImpact : ObjectVisuals
    {
        [SerializeField]
        private ParticleSystem _hitParticleSystem;
        
        void Start()
        {
            InstantiateAndPlayVisualEffect(_hitParticleSystem);

            Destroy(gameObject, _hitParticleSystem.main.duration);
        }
    }
}
