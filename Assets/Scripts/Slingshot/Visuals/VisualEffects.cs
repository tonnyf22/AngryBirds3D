using UnityEngine;
using AngryBirds3D.Visuals;

namespace AngryBirds3D.Slingshot.Visuals
{
    public class VisualEffects : ObjectVisuals
    {
        [SerializeField]
        private SlingshotSpringInput _slingshotSpringInput;

        [SerializeField]
        private ParticleSystem _releaseParticleSystem;

        void OnEnable()
        {
            _slingshotSpringInput.InitiateReleaseLogicEvent += PlayReleaseEffect;
        }

        private void PlayReleaseEffect()
        {
            InstantiateAndPlayVisualEffect(_releaseParticleSystem);
        }

        void OnDisable()
        {
            _slingshotSpringInput.InitiateReleaseLogicEvent -= PlayReleaseEffect;
        }
    }
}
