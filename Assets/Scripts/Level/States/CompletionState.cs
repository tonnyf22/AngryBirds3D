using AngryBirds3D.Level.Audio;
using AngryBirds3D.Level.Score;
using AngryBirds3D.Level.Visuals;
using AngryBirds3D.UserInterface.Level;
using UnityEngine;

namespace AngryBirds3D.Level.States
{
    public class CompletionState : MonoBehaviour
    {
        [SerializeField]
        private PauseManager _pauseManager;

        [SerializeField]
        private AudioEffects _audioEffects;
        [SerializeField]
        private VisualEffects _visualEffects;

        [SerializeField]
        private ScoreSystem _scoreSystem;

        [SerializeField]
        private LevelResults _levelResults;

        void OnEnable()
        {
            _pauseManager.SetPause();

            _scoreSystem.CalculateCurrentSessionLevelScoreAndSaveBest();

            if (_levelResults.IsSuccessfullyPassed())
            {
                _audioEffects.PlaySuccessfulPassClip();
                _visualEffects.PlaySuccessfulPassEffect();
            }
            else
            {
                _audioEffects.PlayNotPassClip();
            }

            _levelResults.ShowLevelResults();
        }
    }
}
