using System.Collections;
using System.Collections.Generic;
using AngryBirds3D.Audio;
using AngryBirds3D.Birds;
using AngryBirds3D.Slingshot;
using UnityEngine;

namespace AngryBirds3D.Throwables.Birds.Audio
{
    public class AudioEffects : ObjectAudio
    {
        private SlingshotSpringInput _slingshotSpringInput;
        [SerializeField]
        private BirdAbility _birdAbility;
        [SerializeField]
        private BirdLifeEndManager _hitManager;

        [SerializeField]
        [Range(1, 20)]
        private int _idleClipMaxRepeatTime;
        [SerializeField]
        private List<AudioClip> _idleClips;
        [SerializeField]
        private List<AudioClip> _flyClips;
        [SerializeField]
        private List<AudioClip> _abilityActivationClips;
        [SerializeField]
        private List<AudioClip> _hitClips;

        private bool _isNotThrown = true;

        private bool isNotWaitingIdleClipDelay = true;

        void Awake()
        {
            SetupSlingshotSpringInput();
        }

        void OnEnable()
        {
            _slingshotSpringInput.InitiateReleaseLogicEvent += ReleaseInitiated;
            _birdAbility.AbilityActivatedEvent += PlayAbilityActivationClip;
            _hitManager.BirdLifeEndedEvent += PlayHitClip;
        }

        private void SetupSlingshotSpringInput()
        {
            _slingshotSpringInput = 
                GameObject.FindWithTag("SlingshotTensionPlane")
                .GetComponent<SlingshotSpringInput>();
        }

        private void ReleaseInitiated()
        {
            _isNotThrown = false;
            PlayFlyClip();
        }

        private void PlayFlyClip()
        {
            AudioClip clip = GetRandomClip(_flyClips);
            _audioSource.PlayOneShot(clip);
        }

        private void PlayAbilityActivationClip()
        {
            AudioClip clip = GetRandomClip(_abilityActivationClips);
            _audioSource.PlayOneShot(clip);
        }

        private void PlayHitClip()
        {
            AudioClip clip = GetRandomClip(_hitClips);
            _audioSource.PlayOneShot(clip);
        }

        private int GenerateIdleClipPlayDelayTime()
        {
            return _random.Next(0, _idleClipMaxRepeatTime + 1);
        }

        void Update()
        {
            if (_isNotThrown)
            {
                if (isNotWaitingIdleClipDelay)
                {
                    StartCoroutine(PlayIdleClip());
                }
            }
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
            _slingshotSpringInput.InitiateReleaseLogicEvent -= ReleaseInitiated;
            _birdAbility.AbilityActivatedEvent -= PlayAbilityActivationClip;
            _hitManager.BirdLifeEndedEvent -= PlayHitClip;
        }
    }
}
