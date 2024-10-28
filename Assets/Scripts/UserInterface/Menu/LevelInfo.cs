using UnityEngine;
using TMPro;
using AngryBirds3D.Level.Score;
using UnityEngine.SceneManagement;
using System.IO;

namespace AngryBirds3D.UserInterface.Menu
{
    public class LevelInfo : MonoBehaviour
    {
        [SerializeField]
        private LevelDataScriptableObject _levelData;

        [SerializeField]
        private TextMeshProUGUI _numberText;

        [SerializeField]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        GameObject[] _starsObjects = new GameObject[3];

        void Awake()
        {
            FillLevelScoreData();
        }

        private void FillLevelScoreData()
        {
            string filePath = CreateFilePathToJSON();

            if (IsFileExists(filePath))
            {
                FillLevelScoreDataFromFile(filePath);
            }
            else
            {
                FillLevelScoreAsNew(filePath);
            }
        }

        private string CreateFilePathToJSON()
        {
            return
                Path.Combine(
                    Application.persistentDataPath, 
                    "LevelResult", 
                    $"{_levelData.LevelNumber}.json");
        }

        private bool IsFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        private void FillLevelScoreDataFromFile(string filePath)
        {
            string json = CreateJSONFromFile(filePath);
            LevelScore levelScore = CreateLevelScoreFromJSON(json);
            _levelData.LevelScore = levelScore;
        }

        private string CreateJSONFromFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        private LevelScore CreateLevelScoreFromJSON(string json)
        {
            return JsonUtility.FromJson<LevelScore>(json);
        }
        
        private void FillLevelScoreAsNew(string filePath)
        {
            Debug.Log("File does not exist: " + filePath);
            Debug.Log("Creating file: " + filePath);

            string directoryPath = CreateDirectoryPath(filePath);
            if (IsDirectoryNotExists(directoryPath))
            {
                CreateDirectory(directoryPath);
            }

            LevelScore levelScore = CreateEmptyLevelScore();
            _levelData.LevelScore = levelScore;

            string json = CreateJSONFromLevelScore(levelScore);

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

        private LevelScore CreateEmptyLevelScore()
        {
            return new LevelScore();
        }

        private string CreateJSONFromLevelScore(LevelScore levelScore)
        {
            return JsonUtility.ToJson(levelScore);
        }

        private void WriteJSONToFile(string json, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (IOException e)
            {
                Debug.LogError("Failed to save JSON data to new file: " + e.Message);
            }
        }

        void Start()
        {
            _numberText.text = _levelData.LevelNumber;
            _scoreText.text = _levelData.LevelScore.Score.ToString();

            for (int i = 0; i < _levelData.LevelScore.StarsScore; i++)
            {
                _starsObjects[i].SetActive(true);
            }
        }

        public void BeginLevel()
        {
            SceneManager.LoadScene(_levelData.LevelScene.Name);
        }
    }
}
