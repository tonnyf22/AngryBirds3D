using AngryBirds3D.Level.Score;
using TMPro;
using UnityEngine;

namespace AngryBirds3D.UserInterface.Level
{
    public class ScoreTracker : MonoBehaviour
    {
        [SerializeField]
        private ScoreSystem _scoreSystem;

        [SerializeField]
        private TextMeshProUGUI _scoreText;

        void OnEnable()
        {
            _scoreSystem.ScoreUpdatedEvent += UpdateScoreUI;
        }

        private void UpdateScoreUI()
        {
            _scoreText.text = _scoreSystem.CurrentSessionLevelScore.Score.ToString();
        }

        void OnDisable()
        {
            _scoreSystem.ScoreUpdatedEvent -= UpdateScoreUI;
        }
    }
}
