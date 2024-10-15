using UnityEngine;
using TMPro;
using AngryBirds3D.Level.Score;
using UnityEngine.SceneManagement;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace AngryBirds3D.UserInterface.Menu
{
    public class LevelInfo : MonoBehaviour
    {
        [SerializeField]
        private LevelDataScriptableObject _levelData;

        [SerializeField]
        private TextMeshProUGUI _NumberText;

        [SerializeField]
        private TextMeshProUGUI _ScoreText;

        [SerializeField]
        GameObject[] _starsObjects = new GameObject[3];

        void Start()
        {
            _NumberText.text = _levelData.LevelNumber;
            _ScoreText.text = _levelData.LevelScore.Score.ToString();

            for (int i = 0; i < _levelData.LevelScore.StarsScore; i++)
            {
                _starsObjects[i].SetActive(true);
            }
        }

        void Update()
        {
            ServeInput();
        }

        private void ServeInput()
        {
            if (Touch.activeTouches.Count > 0)
            {
                Touch firstTouch = Touch.activeTouches[0];

                if (firstTouch.isTap)
                {
                    BeginLevel();
                }
            }
        }

        private void BeginLevel()
        {
            SceneManager.LoadScene(_levelData.LevelScene.BuildIndex);
        }
    }
}
