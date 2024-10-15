using UnityEngine;
using AngryBirds3D.Slingshot;
using AngryBirds3D.GameCamera.FreeMode;

namespace AngryBirds3D.UserInterface.Level
{
    public class CameraModeManager : MonoBehaviour
    {
        private enum CameraMode { Slingshot, Free };

        [SerializeField]
        private SlingshotSpringInput _slingshotSpringInput;
        [SerializeField]
        private MovementInput _movementInput;
        
        [SerializeField]
        private GameObject _cameraSlingshotMode;
        [SerializeField]
        private GameObject _cameraFreeMode;

        private CameraMode _currentCameraMode = CameraMode.Slingshot;

        // disable button when in fly state

        public void ToggleCameraMode()
        {
            switch (_currentCameraMode)
            {
                case CameraMode.Slingshot:
                    GoToFreeModeViewCamera();
                    _currentCameraMode = CameraMode.Free;
                    break;
                case CameraMode.Free:
                    GoToSlingshotViewCamera();
                    _currentCameraMode = CameraMode.Slingshot;
                    break;
            }
        }

        private void GoToSlingshotViewCamera()
        {
            SetActivationsForInputsInSlingshotMode();
            SetActivationsForCamerasInSlingshotMode();
        }

        private void SetActivationsForInputsInSlingshotMode()
        {
            _slingshotSpringInput.enabled = true;
            _movementInput.enabled = false;
        }

        private void SetActivationsForCamerasInSlingshotMode()
        {
            _cameraSlingshotMode.SetActive(true);
            _cameraFreeMode.SetActive(false);
        }

        private void GoToFreeModeViewCamera()
        {
            SetActivationsForInputsInFreeMode();
            SetActivationsForCamerasInFreeMode();
        }

        private void SetActivationsForInputsInFreeMode()
        {
            _slingshotSpringInput.enabled = false;
            _movementInput.enabled = true;
        }

        private void SetActivationsForCamerasInFreeMode()
        {
            _cameraSlingshotMode.SetActive(false);
            _cameraFreeMode.SetActive(true);
        }
    }
}
