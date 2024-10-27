using UnityEngine;

namespace AngryBirds3D.Throwables.Birds.Abilities.EggThrowable
{
    [RequireComponent(typeof(SphereCollider))]
    public class EggThrowable : BirdAbility
    {
        [SerializeField]
        private BirdLifeEndManager _birdLifeEndManager;

        [SerializeField]
        private GameObject _eggPrefab;

        [SerializeField]
        [Range(0.0f, 200.0f)]
        private float _eggThrowingRecoilImpulseForce;

        private Rigidbody _rb;
        private SphereCollider _sc;

        private Vector3 _eggPosition;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _sc = GetComponent<SphereCollider>();
        }

        protected override void AbilityActivatedByInput()
        {
			CalculatePositionOfEgg();
			InstantiateEgg();

            AddRecoilImpulseToBird();

            _birdLifeEndManager.NotifyAboutBirdLifeEnd();
        }

        private void CalculatePositionOfEgg()
        {
            _eggPosition =
                transform.position +
                transform.localScale.x * _sc.radius * 2.5f * Vector3.down;
        }

        private void InstantiateEgg()
        {
            Instantiate(_eggPrefab, _eggPosition, Quaternion.identity);
        }

        private void AddRecoilImpulseToBird()
        {
            _rb.AddForce(
                _eggThrowingRecoilImpulseForce * Vector3.up,
                ForceMode.Impulse);
        }
    }
}
