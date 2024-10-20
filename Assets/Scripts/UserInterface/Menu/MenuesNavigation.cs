using UnityEngine;

namespace AngryBirds3D.UserInterface.Menu
{
    public class MenuesNavigation : MonoBehaviour
    {
        [SerializeField]
        private GameObject _mainMenu;

        [SerializeField]
        private GameObject _levels;

        [SerializeField]
        private GameObject _settings;
        [SerializeField]
        private GameObject _settingsBackButtonToMainMenu;
        [SerializeField]
        private GameObject _settingsBackButtonToLevels;

        public void OpenMainMenu(GameObject currentMenu)
        {
            currentMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }

        public void OpenLevels(GameObject currentMenu)
        {
            currentMenu.SetActive(false);
            _levels.SetActive(true);
        }

        public void OpenSettingsFromMainMenu(GameObject currentMenu)
        {
            currentMenu.SetActive(false);
            _settings.SetActive(true);

            _settingsBackButtonToMainMenu.SetActive(true);
            _settingsBackButtonToLevels.SetActive(false);
        }

        public void OpenSettingsFromLevels(GameObject currentMenu)
        {
            currentMenu.SetActive(false);
            _settings.SetActive(true);

            _settingsBackButtonToLevels.SetActive(true);
            _settingsBackButtonToMainMenu.SetActive(false);
        }

        public void ExitFromApp()
        {
            Application.Quit();
        }
    }
}
