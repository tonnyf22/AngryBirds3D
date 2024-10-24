using UnityEngine;
using AngryBirds3D.Visuals;

namespace AngryBirds3D.Destroyables.Fortifications.Visuals
{
    public class VisualEffects : ObjectVisuals
    {
        [SerializeField]
        private Fortification _fortification;

        [SerializeField]
        private ParticleSystem _breakParticleSystem;

        void OnEnable()
        {
            _fortification.FortificationDestroyedEvent += PlayBreakEffect;
        }

        private void PlayBreakEffect()
        {
            InstantiateAndPlayVisualEffect(_breakParticleSystem);
        }

        void OnDisable()
        {
            _fortification.FortificationDestroyedEvent -= PlayBreakEffect;
        }
    }
}
