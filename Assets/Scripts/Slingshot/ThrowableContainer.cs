using AngryBirds3D.Throwables.Birds;
using UnityEngine;

namespace AngryBirds3D.Slingshot
{
    public class ThrowableContainer : MonoBehaviour
    {
        [SerializeField]
        private BirdsTrack _birdsTrack;

        [HideInInspector]
        public GameObject CurrentThrowable = null;
        
        public void SetupNewThrowableToShoot()
        {
            ForgetPreviousThrowable();

            CurrentThrowable = _birdsTrack.GetNextBird();

            ZeroCurrentThrowableForShotPointActual();
        }

        private void ForgetPreviousThrowable()
        {
            CurrentThrowable = null;
        }

        private void ZeroCurrentThrowableForShotPointActual()
        {
            CurrentThrowable.transform.position = transform.position;
            CurrentThrowable.transform.parent = transform;
        }

        public void FreeThrowable()
        {
            CurrentThrowable.transform.parent = null;
        }
    }
}
