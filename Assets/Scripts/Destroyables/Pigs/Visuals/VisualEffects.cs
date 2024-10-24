using UnityEngine;
using AngryBirds3D.Visuals;

namespace AngryBirds3D.Destroyables.Pigs.Visuals
{
    public class VisualEffects : ObjectVisuals
    {
        [SerializeField]
        private Pig _pig;

        [SerializeField]
        private ParticleSystem _defeatParticleSystem;

        void OnEnable()
        {
            _pig.PigDefeatedEvent += PlayDefeatEffect;
        }

        private void PlayDefeatEffect()
        {
            InstantiateAndPlayVisualEffect(_defeatParticleSystem);
        }

        void OnDisable()
        {
            _pig.PigDefeatedEvent -= PlayDefeatEffect;
        }
    }
}
