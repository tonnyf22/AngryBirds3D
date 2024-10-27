using System.Collections.Generic;
using AngryBirds3D.Audio;
using UnityEngine;

namespace AngryBirds3D.Level.Audio
{
    public class AudioEffects : ObjectAudio
    {
        [SerializeField]
        private List<AudioClip> _startClips;
        [SerializeField]
        private List<AudioClip> _successfulPassClips;
        [SerializeField]
        private List<AudioClip> _notPassClips;

        void Start()
        {
            AudioClip clip = GetRandomClip(_startClips);

            _audioSource.PlayOneShot(clip);
        }

        public void PlaySuccessfulPassClip()
        {
            AudioClip clip = GetRandomClip(_successfulPassClips);

            _audioSource.PlayOneShot(clip);
        }

        public void PlayNotPassClip()
        {
            AudioClip clip = GetRandomClip(_notPassClips);

            _audioSource.PlayOneShot(clip);
        }
    }
}
