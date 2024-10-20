using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AngryBirds3D.UserInterface.Settings
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField]
        private SettingsScriptableObject _settingsScriptableObject;

        [SerializeField]
        private AudioMixerGroup _audioMixerMusic;
        [SerializeField]
        private AudioMixerGroup _audioMixerSFX;

        [SerializeField]
        private Slider _sliderMusic;
        [SerializeField]
        private Slider _sliderSFX;

        void Start()
        {
            InitialMusicSetup();
            InitialSFXSetup();
        }

        private void InitialMusicSetup()
        {
            _sliderMusic.value = _settingsScriptableObject.VolumeMusic;
            SetMusicVolumeValue(_sliderMusic.value);
        }

        private void InitialSFXSetup()
        {
            _sliderSFX.value = _settingsScriptableObject.VolumeSFX;
            SetSFXVolumeValue(_sliderSFX.value);
        }

        public void SetupMusicVolume()
        {
            float volume = _sliderMusic.value;
            SetMusicVolumeValue(volume);
            SaveMusicVolume(volume);
        }

        public void SetupSFXVolume()
        {
            float volume = _sliderSFX.value;
            SetSFXVolumeValue(volume);
            SaveSFXVolume(volume);
        }

        private void SetMusicVolumeValue(float value)
        {
            _audioMixerMusic.audioMixer.SetFloat("volumeMusic", value);
        }

        private void SetSFXVolumeValue(float value)
        {
            _audioMixerSFX.audioMixer.SetFloat("volumeSFX", value);
        }

        private void SaveMusicVolume(float value)
        {
            _settingsScriptableObject.VolumeMusic = value;
        }

        private void SaveSFXVolume(float value)
        {
            _settingsScriptableObject.VolumeSFX = value;
        }
    }
}
