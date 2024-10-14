using System;

namespace AngryBirds3D.Level.Score
{
    [Serializable]
    public class LevelScore
    {
        public bool IsLevelPassed;

        public int Score;

        public int DefeatedPigsScore;
        public int DestroyedFortificationsScore;
        public int SavedBirdsScore;

        public int StarsScore;
    }
}
