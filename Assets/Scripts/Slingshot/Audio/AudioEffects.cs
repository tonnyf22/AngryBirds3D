using System.Collections.Generic;
using AngryBirds3D.Audio;
using UnityEngine;

namespace AngryBirds3D.Slingshot.Audio
{
    public class AudioEffects : ObjectAudio
    {
        [SerializeField]
        private SlingshotSpringInput _slingshotSpringInput;

        [SerializeField]
        private List<AudioClip> _firstTensionClips;
        [SerializeField]
        private List<AudioClip> _releaseClips;

        void OnEnable()
        {
            _slingshotSpringInput.FirstTensionMadeEvent += PlayFirstTensionClip;
            _slingshotSpringInput.InitiateReleaseLogicEvent += PlayReleaseClip;
        }

        private void PlayFirstTensionClip()
        {
            AudioClip clip = GetRandomClip(_firstTensionClips);

            _audioSource.PlayOneShot(clip);
        }

        private void PlayReleaseClip()
        {
            AudioClip clip = GetRandomClip(_releaseClips);

            _audioSource.PlayOneShot(clip);
        }

        void OnDisable()
        {
            _slingshotSpringInput.FirstTensionMadeEvent -= PlayFirstTensionClip;
            _slingshotSpringInput.InitiateReleaseLogicEvent -= PlayReleaseClip;
        }
    }
}
