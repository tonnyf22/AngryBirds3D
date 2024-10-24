using System;
using AngryBirds3D.Throwables;
using UnityEngine;

namespace AngryBirds3D.Birds.Abilities.Triplication
{
    public class HitManagerPhysicsOnly : MonoBehaviour
    {
        public event Action OnHitOccuredEvent;

        [SerializeField]
        private ForwardLookManager _forwardLookManager;

        void OnCollisionEnter(Collision collision)
        {
            OnHitOccuredEvent?.Invoke();
            _forwardLookManager.enabled = false;
        }
    }
}
