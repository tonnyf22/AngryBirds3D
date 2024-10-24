using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace AngryBirds3D.Audio
{
    public class ObjectAudio : MonoBehaviour
    {
        [SerializeField]
        protected AudioSource _audioSource;
        
        protected Random _random = new Random();

        protected AudioClip GetRandomClip(List<AudioClip> audioClips)
        {
            int clipIndex = GenerateRandomClipIndex(audioClips);
            return audioClips[clipIndex];
        }

        private int GenerateRandomClipIndex(List<AudioClip> audioClips)
        {
            return _random.Next(0, audioClips.Count);
        }
    }
}
