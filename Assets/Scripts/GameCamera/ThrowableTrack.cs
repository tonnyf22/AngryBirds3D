using UnityEngine;
using AngryBirds3D.Slingshot;

namespace AngryBirds3D.GameCamera
{
    public class ThrowableTrack : MonoBehaviour
    {
        [SerializeField]
        private ThrowableContainer _throwableContainer;

        [SerializeField]
        [Range(1.0f, 10.0f)]
        [Tooltip("How fast does camera approach the trowable in fly")]
        private float _interpolationSpeed;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        private float _minimumDistanceToThrowable;

        [SerializeField]
        private Transform DefaultCameraTransform;

        void Update()
        {
            if (IsPositioNotCloseEnoughToThrowable(transform.position))
            {
                InterpolatePositionTowardsTheThrowable();
            }
        }

        private bool IsPositioNotCloseEnoughToThrowable(Vector3 position)
        {
            float distance =
                Vector3.Distance(
                    position,
                    _throwableContainer.CurrentThrowable.transform.position
                );

            return distance > _minimumDistanceToThrowable;
        }

        private void InterpolatePositionTowardsTheThrowable()
        {
            Vector3 potentialPosition = 
                CalculatePotentialPositionAfterInterpolation();

            if (IsPositioNotCloseEnoughToThrowable(potentialPosition))
            {
                transform.position = potentialPosition;
            }
            else
            {
                SetDistanceAsMinimumAllowed();
            }
        }

        private Vector3 CalculatePotentialPositionAfterInterpolation()
        {
            return 
                Vector3.Slerp(
                    transform.position,
                    _throwableContainer.CurrentThrowable.transform.position,
                    _interpolationSpeed / 100.0f
                );
        }

        private void SetDistanceAsMinimumAllowed()
        {
            Vector3 fromCurrentToThrowable = 
                _throwableContainer.CurrentThrowable.transform.position - 
                transform.position;
            Vector3 shortenedToAllowedDistance = 
                Vector3.ClampMagnitude(
                    fromCurrentToThrowable, 
                    _minimumDistanceToThrowable);
            Vector3 calculatedCameraPosition = 
                _throwableContainer.CurrentThrowable.transform.position - 
                shortenedToAllowedDistance;

            transform.position = calculatedCameraPosition;
        }

        public void RestoreDefaultTransform()
        {
            transform.SetPositionAndRotation(
                DefaultCameraTransform.transform.position,
                DefaultCameraTransform.transform.rotation
            );
        }
    }
}
