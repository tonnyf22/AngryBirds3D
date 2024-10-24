using System.Collections.Generic;
using AngryBirds3D.Audio;
using AngryBirds3D.Birds.Abilities.Triplication;
using UnityEngine;

namespace AngryBirds3D.Throwables.Birds.Abilities.Triplication.Audio
{
    public class AudioEffectsPhysicsOnly : ObjectAudio
    {
        [SerializeField]
        private HitManagerPhysicsOnly _hitManagerPhysicsOnly;

        [SerializeField]
        private List<AudioClip> _hitClips;

        void OnEnable()
        {
            _hitManagerPhysicsOnly.OnHitOccuredEvent += PlayHitClip;
        }

        private void PlayHitClip()
        {
            AudioClip clip = GetRandomClip(_hitClips);

            _audioSource.PlayOneShot(clip);
        }

        void OnDisable()
        {
            _hitManagerPhysicsOnly.OnHitOccuredEvent -= PlayHitClip;
        }
    }
}