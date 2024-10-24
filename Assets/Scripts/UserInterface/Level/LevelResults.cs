using UnityEngine;
using TMPro;
using AngryBirds3D.Level.Score;

namespace AngryBirds3D.UserInterface.Level
{
    public class LevelResults : MonoBehaviour
    {
        [SerializeField]
        private GameObject _music;
        [SerializeField]
        private GameObject _liveLevelUI;
        [SerializeField]
        private GameObject _resultsUI;

        [SerializeField]
        private LevelDataScriptableObject _levelData;
        [SerializeField]
        private ScoreSystem _scoreSystem;

        [SerializeField]
        private TextMeshProUGUI _numberText;
        [SerializeField]
        private TextMeshProUGUI _scoreText;
        [SerializeField]
        private TextMeshProUGUI _fortificationsText;
        [SerializeField]
        private TextMeshProUGUI _pigsText;
        [SerializeField]
        private TextMeshProUGUI _birdsText;

        [SerializeField]
        GameObject[] _starsObjects = new GameObject[3];

        public void ShowLevelResults()
        {
            // TurnOnMusic();
            HideLiveLevelUI();
            ShowResultsUI();
            FillResults();
        }

        public bool IsSuccessfullyPassed()
        {
            return _scoreSystem.CurrentSessionLevelScore.IsLevelPassed;
        }

        private void TurnOnMusic()
        {
            _music.SetActive(true);
        }

        private void HideLiveLevelUI()
        {
            _liveLevelUI.SetActive(false);
        }

        private void ShowResultsUI()
        {
            _resultsUI.SetActive(true);
        }

        private void FillResults()
        {
            LevelScore ls = _scoreSystem.CurrentSessionLevelScore;

            _numberText.text = _levelData.LevelNumber;
            _scoreText.text = ls.Score.ToString();
            _fortificationsText.text = ls.DestroyedFortificationsScore.ToString();
            _pigsText.text = ls.DefeatedPigsScore.ToString();
            _birdsText.text = ls.SavedBirdsScore.ToString();

            for (int i = 0; i < ls.StarsScore; i++)
            {
                _starsObjects[i].SetActive(true);
            }
        }
    }
}