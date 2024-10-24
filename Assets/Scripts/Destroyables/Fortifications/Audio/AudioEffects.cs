using System.Collections.Generic;
using AngryBirds3D.Audio;
using UnityEngine;

namespace AngryBirds3D.Destroyables.Fortifications.Audio
{
    public class AudioEffects : ObjectAudio
    {
        [SerializeField]
        private Fortification _fortification;

        [SerializeField]
        private List<AudioClip> _breakClips;

        private AudioClip _clip;

        void OnEnable()
        {
            _fortification.FortificationDestroyedEvent += OnFortificationDestroy;
        }

        private void OnFortificationDestroy()
        {
            RemoveParent();

            PlayDestroymentClip();

            Destroy(_audioSource.gameObject, _clip.length);
        }

        private void RemoveParent()
        {
            _audioSource.transform.parent = null;
        }

        private void PlayDestroymentClip()
        {
            _clip = GetRandomClip(_breakClips);
            _audioSource.PlayOneShot(_clip);
        }

        void OnDisable()
        {
            _fortification.FortificationDestroyedEvent -= PlayDestroymentClip;
        }
    }
}
