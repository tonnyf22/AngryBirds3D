using System.IO;
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
			LoadVolumeDataFromMemory();

			InitialMusicSetup();
			InitialSFXSetup();
		}

		private void LoadVolumeDataFromMemory()
		{
			string filePath = CreateFilePathToJSON();

			if (IsFileExists(filePath))
			{
				FillSettingsFromFile(filePath);
			}
			else
			{
				FileSettingsAsNew(filePath);
			}
		}

		private string CreateFilePathToJSON()
		{
			return
				Path.Combine(
					Application.persistentDataPath,
					"Settings",
					"audio.json");
		}

		private bool IsFileExists(string filePath)
		{
			return File.Exists(filePath);
		}

		private void FillSettingsFromFile(string filePath)
		{
			string json = CreateJSONFromFile(filePath);

			AudioSettingsData audioSettingsData = 
				CreateAudioSettingsDataFromJSON(json);
			FillSettingsScriptableObject(audioSettingsData);
		}

		private string CreateJSONFromFile(string filePath)
		{
			return File.ReadAllText(filePath);
		}

		private AudioSettingsData CreateAudioSettingsDataFromJSON(string json)
		{
			return JsonUtility.FromJson<AudioSettingsData>(json);
		}

		private void FillSettingsScriptableObject(
			AudioSettingsData audioSettingsData)
		{
			_settingsScriptableObject.VolumeSFX = audioSettingsData.VolumeSFX;
			_settingsScriptableObject.VolumeMusic = audioSettingsData.VolumeMusic;
		}

		private void FileSettingsAsNew(string filePath)
		{
            Debug.Log("File does not exist: " + filePath);
            Debug.Log("Creating file: " + filePath);

            string directoryPath = CreateDirectoryPath(filePath);
            if (IsDirectoryNotExists(directoryPath))
            {
				CreateDirectory(directoryPath);
            }

            AudioSettingsData audioSettingsData = 
				CreateEmptyAudioSettingsDataFromSlidersValues();

            string json = CreateJSONFromAudioSettingsData(audioSettingsData);

			WriteJSONToFile(json, filePath);
		}

		private string CreateDirectoryPath(string filePath)
		{
			return Path.GetDirectoryName(filePath);
		}

		private bool IsDirectoryNotExists(string directoryPath)
		{
			return !Directory.Exists(directoryPath);
		}

		private void CreateDirectory(string directoryPath)
		{
			Directory.CreateDirectory(directoryPath);
		}

		private AudioSettingsData CreateEmptyAudioSettingsDataFromSlidersValues()
		{
			return new AudioSettingsData()
            {
                VolumeSFX = _sliderSFX.value,
                VolumeMusic = _sliderMusic.value
            };
		}

		private string CreateJSONFromAudioSettingsData(
			AudioSettingsData audioSettingsData)
		{
			return JsonUtility.ToJson(audioSettingsData);
		}

		private void WriteJSONToFile(string json, string filePath)
		{
            try
            {
                File.WriteAllText(filePath, json);

                Debug.Log("Successfully saved JSON data to new file: " + filePath);
            }
            catch (IOException e)
            {
                Debug.LogError("Failed to save JSON data to new file: " + e.Message);
            }
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

		public void SaveVolumeDataToMemory()
		{
			AudioSettingsData audioSettingsData = 
				CreateAudioSettingsDataFromScriptableObject();

			string json = CreateJSONFromAudioSettingsData(audioSettingsData);

			string filePath = CreateFilePathToJSON();

			WriteJSONToFile(json, filePath);
		}

		private AudioSettingsData CreateAudioSettingsDataFromScriptableObject()
		{
			return new AudioSettingsData()
			{
                VolumeSFX = _settingsScriptableObject.VolumeSFX,
                VolumeMusic = _settingsScriptableObject.VolumeMusic
			};
		}
	}
}
