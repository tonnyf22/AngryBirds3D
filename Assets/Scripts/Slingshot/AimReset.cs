using AngryBirds3D.Level.States;
using UnityEngine;

namespace AngryBirds3D.Slingshot
{
	public class AimReset : MonoBehaviour
	{
		[SerializeField]
		private SlingshotSpringInput _slingshotSpringInput;
		[SerializeField]
		private SpringTension _springTension;

        [SerializeField]
        private LevelSM _levelSM;

        public void ResetSlingshotAimOnAimState()
        {
			if (_levelSM.IsSlingshotAimStateEnabled())
			{
				print("active");
                ResetSlingshotAim();
			}
        }

		private void ResetSlingshotAim()
		{
			_springTension.RestoreTrajectoryPrediction();
			_springTension.RestoreShotPointActual();
			_slingshotSpringInput.ResetInput();
		}
	}
}
