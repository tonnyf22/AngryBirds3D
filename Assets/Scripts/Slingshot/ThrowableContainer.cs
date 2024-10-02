using UnityEngine;

namespace Slingshot
{
    public class ThrowableManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _throwablesBag;

        [HideInInspector]
        public GameObject CurrentThrowable = null;
        
        public Transform ShotPointActual;
        public Transform ShotPointReference;

        [SerializeField]
        [Range(30.0f, 100.0f)]
        private float _tensionForcePerDistanceUnit;

        void Start()
        {
            SetupNewThrowableToShoot();
        }

        public void SetupNewThrowableToShoot()
        {
            if (_throwablesBag.transform.childCount > 0)
            {
                CurrentThrowable =
                    _throwablesBag.transform.GetChild(0).gameObject;
                CurrentThrowable.transform.position = 
                    transform.position;

                CurrentThrowable.transform.parent = transform;
            }
        }

        public void ReleaseCurrentThrowable()
        {
            Rigidbody rigidbody = SetUpPhysicsForThrowable();

            float stretchDistance = CalculateDistanceToShotReference();
            Vector3 forceDirection = CalculateAppliedForceDirection();

            FreeThrowable();
            ApplyImpulseToThrowable(rigidbody, forceDirection, stretchDistance);
            ForgetThrowable();
        }

        private Rigidbody SetUpPhysicsForThrowable()
        {
            CurrentThrowable.AddComponent<SphereCollider>();

            Rigidbody rb = CurrentThrowable.AddComponent<Rigidbody>();
            rb.mass = 6;
            rb.angularDrag = 3;

            return rb;
        }

        private float CalculateDistanceToShotReference()
        {
            float distance = 
                Vector3.Distance(
                    ShotPointReference.position,
                    ShotPointActual.position
                );

            Debug.Log("Distance: " + distance);

            return distance;
        }

        private Vector3 CalculateAppliedForceDirection()
        {
            Vector3 direction =
                (ShotPointReference.position - ShotPointActual.position)
                .normalized;

            return direction;
        }

        private void FreeThrowable()
        {
            CurrentThrowable.transform.parent = null;
        }

        private void ApplyImpulseToThrowable(
            Rigidbody rigidbody,
            Vector3 forceDirection,
            float stretchDistance)
        {
            rigidbody.AddForce(
                _tensionForcePerDistanceUnit * stretchDistance *
                forceDirection);
        }

        private void ForgetThrowable()
        {
            CurrentThrowable = null;
        }
    }
}
