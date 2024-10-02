using UnityEngine;

namespace Slingshot
{
    public class ThrowableManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _throwablesBag;

        [HideInInspector]
        public GameObject CurrentThrowable = null;
        
        void Start()
        {
            SetupNewThrowableToShoot();
        }

        public void SetupNewThrowableToShoot()
        {
            if (_throwablesBag.transform.childCount > 0)
            {
                CurrentThrowable =
                    _throwablesBag.transform.GetChild(0).gameObject;
                CurrentThrowable.transform.position = 
                    transform.position;

                CurrentThrowable.transform.parent = transform;
            }
        }

        public void FreeThrowable()
        {
            CurrentThrowable.transform.parent = null;
        }

        public void ForgetThrowable()
        {
            CurrentThrowable = null;
        }
    }
}
