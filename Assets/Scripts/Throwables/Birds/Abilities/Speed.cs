using System;
using UnityEngine;

namespace AngryBirds3D.Birds.Abilities
{
	[RequireComponent(typeof(SphereCollider))]
    public class Speed : BirdAbility
    {
        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float _speedToAddImmediately;

        private Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        protected override void AbilityActivatedByInput()
        {
            _rb.AddForce(
                _rb.mass * _speedToAddImmediately * transform.forward,
                ForceMode.Impulse);
        }
    }
}
