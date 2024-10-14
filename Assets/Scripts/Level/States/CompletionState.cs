using AngryBirds3D.Level.Score;
using UnityEngine;

namespace AngryBirds3D.Level.States
{
    public class CompletionState : MonoBehaviour
    {
        [SerializeField]
        private ScoreSystem _scoreSystem;

        void OnEnable()
        {
            // vfx salute
            // sfx and / or music

            _scoreSystem.CalculateCurrentSessionLevelScoreAndSaveBest();

            LevelScore ls = _scoreSystem.CurrentSessionLevelScore ;

            print("is level passed: " + ls.IsLevelPassed);
            print("score: " + ls.Score);
            print("defeated pigs score: " + ls.DefeatedPigsScore);
            print("destroyed fortifications score: " + ls.DestroyedFortificationsScore);
            print("stars score: " + ls.StarsScore);

            // show score on ui (with event or what ??? think about this, ok ...)

        }

        void OnDisable()
        {
            // only reload / quit to menu after, dk whether need this method
        }
    }
}
