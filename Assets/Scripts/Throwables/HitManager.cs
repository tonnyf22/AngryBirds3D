using System;
using System.Collections;
using UnityEngine;

namespace AngryBirds3D.Throwables
{
    public class HitManager : MonoBehaviour
    {
        public event Action OnHitOccuredEvent;
        public event Action DelayFinishedEvent;

        [SerializeField]
        [Range(1.0f, 5.0f)]
        [Tooltip("Delay in seconds to wait after bird was hit before changing state to \"Pass Check State\"")]
        private float _dealyAfterHit;

        void OnCollisionEnter(Collision collision)
        {
            OnHitOccuredEvent?.Invoke();
        }

        public void StartDelayAfterHit()
        {
            StartCoroutine(DelayAfterHit());
        }

        private IEnumerator DelayAfterHit()
        {
            yield return new WaitForSeconds(_dealyAfterHit);

            DelayFinishedEvent?.Invoke();
        }
    }
}
