using UnityEngine;
using AngryBirds3D.Visuals;

namespace AngryBirds3D.Level.Visuals
{
    public class VisualEffects : ObjectVisuals
    {
        [SerializeField]
        private ParticleSystem _successfulPassParticleSystem;

        public void PlaySuccessfulPassEffect()
        {
            InstantiateAndPlayVisualEffect(_successfulPassParticleSystem);
        }
    }
}
