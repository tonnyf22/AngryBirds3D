using System.Collections.Generic;
using AngryBirds3D.Audio;
using UnityEngine;

namespace AngryBirds3D.Throwables.Birds.Abilities.Explosion.Audio
{
    public class AudioEffectsImpact : ObjectAudio
    {
        [SerializeField]
        private List<AudioClip> _explosionClips;

        private AudioClip _clip;
        
        void Start()
        {
            PlayClip();

            Destroy(gameObject, _clip.length);
        }

        private void PlayClip()
        {
            _clip = GetRandomClip(_explosionClips);

            _audioSource.PlayOneShot(_clip);
        }
    }
}
