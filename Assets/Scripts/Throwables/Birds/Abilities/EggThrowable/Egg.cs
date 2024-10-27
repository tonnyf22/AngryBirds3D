using UnityEngine;

namespace AngryBirds3D.Throwables.Birds.Abilities.EggThrowable
{
    public class Egg : MonoBehaviour
    {
        [SerializeField]
        private GameObject _impactPrefab;

        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float _eggThrowingImpulseForce;

        private Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.AddForce(
                _eggThrowingImpulseForce * Vector3.down,
                ForceMode.Impulse);
        }

        void OnCollisionEnter(Collision collision)
        {
            Instantiate(_impactPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
            // gameObject.SetActive(false);
        }
    }
}
