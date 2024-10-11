using UnityEngine;
using AngryBirds3D.Slingshot;

namespace AngryBirds3D.GameCamera.Slingshot
{
    public class ThrowableTrack : MonoBehaviour
    {
        [SerializeField]
        private ThrowableContainer _throwableContainer;

        [SerializeField]
        [Range(0.0f, 10.0f)]
        [Tooltip("How fast does camera approach the trowable in fly")]
        private float _positionInterpolationSpeed;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        private float _minimumDistanceToThrowable;
        // [SerializeField]
        // [Range(1.0f, 20.0f)]
        // [Tooltip("How fast does camera turn in the direction of the trowable in fly")]
        // private float _rotationInterpolationSpeed;

        [SerializeField]
        private Transform DefaultCameraTransform;

        void Update()
        {
            if (IsPositioNotCloseEnoughToThrowable(transform.position))
            {
                InterpolatePositionTowardsTheThrowable();
            }

            InterpolateRotationTowardsTheThrowable();
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
            float interpolatoinRatio = 
                    _positionInterpolationSpeed * 
                    Time.deltaTime;

            return 
                Vector3.Slerp(
                    transform.position,
                    _throwableContainer.CurrentThrowable.transform.position,
                    interpolatoinRatio
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

        private void InterpolateRotationTowardsTheThrowable()
        {
            // Quaternion fullRotationTowardsThrowable = 
            //     CalculateFullRotationTowardsThrowable();

            // float interpolatoinRatio = 
            //         _rotationInterpolationSpeed * 
            //         Time.deltaTime /
            //         10.0f;

            transform.rotation = 
                Quaternion.LookRotation(
                    _throwableContainer.CurrentThrowable.transform.position - 
                    transform.position);

            // transform.rotation = 
            //     Quaternion.Slerp(
            //         transform.rotation,
            //         fullRotationTowardsThrowable,
            //         interpolatoinRatio
            //     );
        }

        // private Quaternion CalculateFullRotationTowardsThrowable()
        // {
        //     Vector3 toDirection = 
        //         _throwableContainer.CurrentThrowable.transform.position - 
        //         transform.position;

        //     return 
        //         Quaternion.FromToRotation(
        //             transform.forward,
        //             toDirection
        //         );
        // }

        public void RestoreDefaultTransform()
        {
            transform.SetPositionAndRotation(
                DefaultCameraTransform.transform.position,
                DefaultCameraTransform.transform.rotation
            );
        }
    }
}
