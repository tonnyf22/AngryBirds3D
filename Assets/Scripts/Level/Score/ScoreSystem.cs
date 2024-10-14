using AngryBirds3D.Birds;
using AngryBirds3D.Destroyables.Fortifications;
using AngryBirds3D.Destroyables.Pigs;
using AngryBirds3D.Throwables.Birds;
using UnityEngine;

namespace AngryBirds3D.Level.Score
{
    public class ScoreSystem : MonoBehaviour
    {
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
        }

        private void ScoreFortification(int score)
        {
            _currentSessionLevelScore.DestroyedFortificationsScore += score;
        }

        public void CalculateCurrentSessionLevelScoreAndSaveBest()
        {
            CurrentSessionResultsCalculations();
            SaveBest();
        }

        private void CurrentSessionResultsCalculations()
        {
            // score (pigs and fortifications only)
            _currentSessionLevelScore.Score = 
                _currentSessionLevelScore.DefeatedPigsScore +
                _currentSessionLevelScore.DestroyedFortificationsScore;

            // is passed / stars
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
                    .GetBirdByIndex(i)
                    .GetComponent<BirdAbility>()
                    .PointsForSave;
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

        private void SaveBest()
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
