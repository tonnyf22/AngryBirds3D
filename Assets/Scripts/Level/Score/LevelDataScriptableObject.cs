using Udar.SceneManager;
using UnityEngine;

namespace AngryBirds3D.Level.Score
{
    [CreateAssetMenu(
        fileName = "LevelData", 
        menuName = "ScriptableObjects/LevelDataScriptableObject", 
        order = 1)]
    public class LevelDataScriptableObject : ScriptableObject
    {
        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        public string LevelNumber;

        public LevelScore LevelScore;

        public StarsByScore StarsByScore;

        public SceneField LevelScene;
    }
}
