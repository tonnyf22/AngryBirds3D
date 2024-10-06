using UnityEngine;

namespace AngryBirds3D.Birds.Abilities
{
    public class Speed : BirdAbility
    {
        [SerializeField]
        // set clever [Range(0.0f, 1.0f)]
        private float _speedToAddImmediately;

        private Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        protected override void AbilityActivated()
        {
            _rb.AddForce(
                transform.forward * _speedToAddImmediately,
                ForceMode.Impulse);
        }
    }
}
