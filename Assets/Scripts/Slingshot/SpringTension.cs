using System;
using AngryBirds3D.Managers;
using UnityEngine;

namespace AngryBirds3D.Slingshot
{
    public class SpringTension : MonoBehaviour
    {
        [SerializeField]
        private ThrowableManager _throwableContainerScript;

        [SerializeField]
        private SlingshotSpringInput _slingshotSpringInput;

        [SerializeField]
        private Transform _shotPointActual;
        private Vector3 _shotPointActualInitPosition;
        [SerializeField]
        private Transform _shotPointReference;

        [SerializeField]
        [Range(30.0f, 100.0f)]
        private float _tensionForcePerDistanceUnit;

        void Start()
        {
            _shotPointActualInitPosition = _shotPointActual.position;
        }

        void OnEnable()
        {
            _slingshotSpringInput
                .ChangeThrowablePositionEvent += ChangeThrowablePosition;
            _slingshotSpringInput
                .InitiateReleaseLogicEvent += InitiateReleaseLogic;
        }

        void OnDisable()
        {
            _slingshotSpringInput
                .ChangeThrowablePositionEvent -= ChangeThrowablePosition;
            _slingshotSpringInput
                .InitiateReleaseLogicEvent -= InitiateReleaseLogic;
        }

        private void ChangeThrowablePosition(Vector3 position)
        {
           _shotPointActual.position = position;
        }

        private void InitiateReleaseLogic()
        {
            ReleaseCurrentThrowable();

            // separate calls, but together for now

            _throwableContainerScript.SetupNewThrowableToShoot();

            RestoreShotPointActualInitPosition();
        }

        private void ReleaseCurrentThrowable()
        {
            Rigidbody rigidbody = SetUpPhysicsForThrowable();

            float stretchDistance = CalculateDistanceToShotReference();
            Vector3 forceDirection = CalculateAppliedForceDirection();

            _throwableContainerScript.FreeThrowable();
            ApplyImpulseToThrowable(rigidbody, forceDirection, stretchDistance);
            _throwableContainerScript.ForgetThrowable();
        }

        private Rigidbody SetUpPhysicsForThrowable()
        {
            _throwableContainerScript.CurrentThrowable.AddComponent<SphereCollider>();

            // get this data from the actual throwable
            Rigidbody rb = _throwableContainerScript.CurrentThrowable.AddComponent<Rigidbody>();
            rb.mass = 6;
            rb.angularDrag = 3;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            return rb;
        }

        private float CalculateDistanceToShotReference()
        {
            float distance = 
                Vector3.Distance(
                    _shotPointReference.position,
                    _shotPointActual.position
                );

            // Debug.Log("Distance: " + distance);

            return distance;
        }

        private Vector3 CalculateAppliedForceDirection()
        {
            Vector3 direction =
                (_shotPointReference.position - _shotPointActual.position)
                .normalized;

            return direction;
        }

        private void ApplyImpulseToThrowable(
            Rigidbody rigidbody,
            Vector3 forceDirection,
            float stretchDistance)
        {
            rigidbody.AddForce(
                _tensionForcePerDistanceUnit * stretchDistance *
                forceDirection,
                ForceMode.Impulse);
        }

        private void RestoreShotPointActualInitPosition()
        {
            _shotPointActual.position = _shotPointActualInitPosition;
        }
    }
}
