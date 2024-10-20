using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace AngryBirds3D.UserInterface.Level
{
    public class PauseManager : MonoBehaviour
    {
        public void SetPause()
        {
            DisableTouch();
            SetPauseTimeSpeed();
        }

        public void SetLive()
        {
            SetLiveTimeSpeed();
            EnableTouch();
        }

        private void DisableTouch()
        {
            EnhancedTouchSupport.Disable();
        }

        private void SetPauseTimeSpeed()
        {
            Time.timeScale = 0.0f;
        }

        private void SetLiveTimeSpeed()
        {
            Time.timeScale = 1.0f;
        }

        private void EnableTouch()
        {
            EnhancedTouchSupport.Enable();
        }
    }
}
