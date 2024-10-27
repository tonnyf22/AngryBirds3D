using UnityEngine;

namespace AngryBirds3D.Throwables.Birds.Abilities.Explosion
{
    public class Explosion : BirdAbility
    {
        [SerializeField]
        private BirdLifeEndManager _birdLifeEndManager;

        [SerializeField]
        private GameObject _impactPrefab;

        protected override void AbilityActivatedByInput()
        {
            Instantiate(_impactPrefab, transform.position, Quaternion.identity);

            _birdLifeEndManager.NotifyAboutBirdLifeEnd();

            MockBirdDestroyment();
        }

        private void MockBirdDestroyment()
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<SphereCollider>().enabled = false;

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
