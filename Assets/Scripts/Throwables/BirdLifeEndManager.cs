using System;
using System.Collections;
using UnityEngine;

namespace AngryBirds3D.Throwables
{
    public class BirdLifeEndManager : MonoBehaviour
    {
        public event Action BirdLifeEndedEvent;
        public event Action DelayFinishedEvent;

        [SerializeField]
        [Range(1.0f, 5.0f)]
        [Tooltip("Delay in seconds to wait after bird was hit before changing state to \"Pass Check State\"")]
        private float _dealyAfterHit;

        void OnCollisionEnter(Collision collision)
        {
            BirdLifeEndedEvent?.Invoke();

            // #idea: add separate event which is invoked on specific collision force
        }

        public void NotifyAboutBirdLifeEnd()
        {
            BirdLifeEndedEvent?.Invoke();
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
