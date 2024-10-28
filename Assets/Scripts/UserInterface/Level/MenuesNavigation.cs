using UnityEngine;
using Udar.SceneManager;
using UnityEngine.SceneManagement;

namespace AngryBirds3D.UserInterface.Level
{
    public class MenuesNavigation : MonoBehaviour
    {
        [SerializeField]
        private SceneField _menuScene;

        [SerializeField]
        private SceneField _previousLevelScene;

        [SerializeField]
        private SceneField _nextLevelScene;

        [SerializeField]
        private GameObject _levelLive;

        [SerializeField]
        private GameObject _pause;

        [SerializeField]
        private GameObject _results;

        public void OpenLevelLive(GameObject currentMenu)
        {
            currentMenu.SetActive(false);
            _levelLive.SetActive(true);
        }

        public void OpenPause(GameObject currentMenu)
        {
            currentMenu.SetActive(false);
            _pause.SetActive(true);
        }

        public void OpenResults(GameObject currentMenu)
        {
            currentMenu.SetActive(false);
            _results.SetActive(true);
        }

        public void LoadPreviousLevel()
        {
            SceneManager.LoadScene(_previousLevelScene.Name);
        }

        public void LoadSameLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(_nextLevelScene.Name);
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(_menuScene.Name);
        }
    }
}
