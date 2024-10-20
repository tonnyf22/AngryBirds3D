using AngryBirds3D.Level.Score;
using AngryBirds3D.UserInterface.Level;
using UnityEngine;

namespace AngryBirds3D.Level.States
{
    public class CompletionState : MonoBehaviour
    {
        [SerializeField]
        private ScoreSystem _scoreSystem;

        [SerializeField]
        private LevelResults _levelResults;

        void OnEnable()
        {
            // vfx salute
            // sfx and / or music

            _scoreSystem.CalculateCurrentSessionLevelScoreAndSaveBest();

            // LevelScore ls = _scoreSystem.CurrentSessionLevelScore ;

            // print("is level passed: " + ls.IsLevelPassed);
            // print("score: " + ls.Score);
            // print("defeated pigs score: " + ls.DefeatedPigsScore);
            // print("destroyed fortifications score: " + ls.DestroyedFortificationsScore);
            // print("stars score: " + ls.StarsScore);
            // print("saved birds score: " + ls.SavedBirdsScore);

            _levelResults.ShowLevelResults();
        }

        void OnDisable()
        {
            // only reload / quit to menu after, dk whether need this method
        }
    }
}
