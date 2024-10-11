using UnityEngine;
using UnityEngine.Animations;

namespace AngryBirds3D.GameCamera.Slingshot
{
    public class LookAtConstraintManager : MonoBehaviour
    {
        [SerializeField]
        private LookAtConstraint _lookAtConstraint;

        void Start()
        {
            _lookAtConstraint.enabled = false;
        }

        void Update()
        {
            enabled = false;
        }
    }
}
