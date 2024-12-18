using System;
using System.IO;
using AngryBirds3D.Destroyables.Fortifications;
using AngryBirds3D.Destroyables.Pigs;
using AngryBirds3D.Throwables.Birds;
using UnityEngine;

namespace AngryBirds3D.Level.Score
{
    public class ScoreSystem : MonoBehaviour
    {
        public event Action ScoreUpdatedEvent;

        [SerializeField]
        private LevelDataScriptableObject _levelData;

        [SerializeField]
        private PigsTrack _pigsTrack;

        [SerializeField]
        private FortificationsTrack _fortificationsTrack;

        [SerializeField]
        private BirdsTrack _birdsTrack;

        private LevelScore _currentSessionLevelScore;
        public LevelScore CurrentSessionLevelScore
        { 
            get { return _currentSessionLevelScore; } 
        }

        void OnEnable()
        {
            _pigsTrack.ScoreEvent += ScorePig;
            _fortificationsTrack.ScoreEvent += ScoreFortification;
        }
        
        void Start()
        {
            _currentSessionLevelScore = new LevelScore();
        }

        private void ScorePig(int score)
        {
            _currentSessionLevelScore.DefeatedPigsScore += score;
            _currentSessionLevelScore.Score += score;
            ScoreUpdatedEvent?.Invoke();
        }

        private void ScoreFortification(int score)
        {
            _currentSessionLevelScore.DestroyedFortificationsScore += score;
            _currentSessionLevelScore.Score += score;
            ScoreUpdatedEvent?.Invoke();
        }

        public void CalculateCurrentSessionLevelScoreAndSaveBest()
        {
            CurrentSessionResultsCalculations();
            SaveBestToMemory();
            SaveBestToScriptableObject();
        }

        private void CurrentSessionResultsCalculations()
        {
            int remainedPigs = _pigsTrack.PigsCount();
            if (remainedPigs > 0)
            {
                _currentSessionLevelScore.IsLevelPassed = false;
                _currentSessionLevelScore.StarsScore = 0;
            }
            else
            {
                _currentSessionLevelScore.SavedBirdsScore = 
                    CalculateSavedBirdsScore();
                _currentSessionLevelScore.Score +=
                    _currentSessionLevelScore.SavedBirdsScore;

                _currentSessionLevelScore.IsLevelPassed = true;
                _currentSessionLevelScore.StarsScore = 
                    CalculateStarsScoreOfCurrentSession();
            }
        }

        private int CalculateSavedBirdsScore()
        {
            int score = 0;
            for (int i = 0; i < _birdsTrack.BirdsCount(); i++)
            {
                score +=
                    _birdsTrack
                    .GetScoreForSavedBirdByIndex(i);
            }
            return score;
        }

        private int CalculateStarsScoreOfCurrentSession()
        {
            int currentScore = _currentSessionLevelScore.Score;

            int levelOneStar = _levelData.StarsByScore.OneStarScore;
            int levelTwoStar = _levelData.StarsByScore.TwoStarScore;
            int levelThreeStar = _levelData.StarsByScore.ThreeStarScore;

            if (currentScore < levelOneStar)
            {
                return 0;
            }
            else if (
                currentScore >= levelOneStar && 
                currentScore < levelTwoStar)
            {
                return 1;
            }
            else if (
                currentScore >= levelTwoStar && 
                currentScore < levelThreeStar)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private void SaveBestToMemory()
        {
            string json = CreateJSONFromLevelScoreData();

            string filePath = CreateFilePathToJSON();
            
            WriteJSONToFile(json, filePath);
        }

        private string CreateJSONFromLevelScoreData()
        {
            return JsonUtility.ToJson(_currentSessionLevelScore);
        }

        private string CreateFilePathToJSON()
        {
            return
                Path.Combine(
                    Application.persistentDataPath, 
                    "LevelResult", 
                    $"{_levelData.LevelNumber}.json");
        }

        private void WriteJSONToFile(string json, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, json);

                Debug.Log("Successfully saved JSON data: " + filePath);
            }
            catch (IOException e)
            {
                Debug.LogError("Failed to save JSON data: " + e.Message);
            }
        }

        private void SaveBestToScriptableObject()
        {
            if (_currentSessionLevelScore.Score > _levelData.LevelScore.Score)
            {
                _levelData.LevelScore = _currentSessionLevelScore;
            }
        }

        void OnDisable()
        {
            _pigsTrack.ScoreEvent -= ScorePig;
            _fortificationsTrack.ScoreEvent -= ScorePig;
        }
    }
}
