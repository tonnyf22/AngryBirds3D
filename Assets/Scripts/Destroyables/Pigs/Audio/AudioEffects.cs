using System.Collections;
using System.Collections.Generic;
using AngryBirds3D.Audio;
using UnityEngine;

namespace AngryBirds3D.Destroyables.Pigs.Audio
{
    public class AudioEffects : ObjectAudio
    {
        [SerializeField]
        private Pig _pig;

        [SerializeField]
        [Range(1, 20)]
        private int _idleClipMaxRepeatTime;
        [SerializeField]
        private List<AudioClip> _idleClips;
        [SerializeField]
        private List<AudioClip> _defeatClips;

        private bool isNotWaitingIdleClipDelay = true;

        private AudioClip _clip;

        void OnEnable()
        {
            _pig.PigDefeatedEvent += OnPigDefeated;
        }

        private void OnPigDefeated()
        {
            RemoveParent();

            PlayDefeatClip();

            Destroy(_audioSource.gameObject, _clip.length);
        }

        private void RemoveParent()
        {
            _audioSource.transform.parent = null;
        }

        private void PlayDefeatClip()
        {
            _clip = GetRandomClip(_defeatClips);
            _audioSource.PlayOneShot(_clip);
        }

        void Update()
        {
            if (isNotWaitingIdleClipDelay)
            {
                StartCoroutine(PlayIdleClip());
            }
        }

        private int GenerateIdleClipPlayDelayTime()
        {
            return _random.Next(0, _idleClipMaxRepeatTime + 1);
        }

        private IEnumerator PlayIdleClip()
        {
            isNotWaitingIdleClipDelay = false;

            AudioClip clip = GetRandomClip(_idleClips);
            _audioSource.PlayOneShot(clip);
            
            int delayTime = GenerateIdleClipPlayDelayTime();
            yield return new WaitForSeconds(delayTime);

            isNotWaitingIdleClipDelay = true;
        }

        void OnDisable()
        {
            _pig.PigDefeatedEvent -= PlayDefeatClip;
        }
    }
}
