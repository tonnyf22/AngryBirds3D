using UnityEngine;

namespace AngryBirds3D.Throwables.Birds.Abilities.ExplosionImpact
{
    [RequireComponent(typeof(SphereCollider))]
    public class Impact : MonoBehaviour
    {
        [SerializeField]
        private GameObject _audioGameObject;
        [SerializeField]
        private GameObject _visualsGameObject;

        [SerializeField]
        [Range(0.1f, 0.5f)]
        private float _impactLifeTime;

        void Start()
        {
            _audioGameObject.transform.parent = null;
            _visualsGameObject.transform.parent = null;

            Destroy(gameObject, _impactLifeTime);
        }
    }
}
