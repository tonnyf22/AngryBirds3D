using AngryBirds3D.Throwables;
using UnityEngine;

namespace AngryBirds3D.Birds.Abilities.Triplication
{
    public class HitManagerPhysicsOnly : MonoBehaviour
    {
        [SerializeField]
        private ForwardLookManager _forwardLookManager;

        void OnCollisionEnter(Collision collision)
        {
            _forwardLookManager.enabled = false;
        }
    }
}
