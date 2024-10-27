using UnityEngine;

namespace AngryBirds3D.Throwables
{
    public class ForwardLookManager : MonoBehaviour
    {
        private Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            // workaround, TBO; suppress zero vector3 assigning to forward
            // seems like impulse application doesn't work immediately
            if (_rb.velocity.magnitude != 0.0f)
            {
                transform.forward = _rb.velocity;
            }
        }
    }
}
