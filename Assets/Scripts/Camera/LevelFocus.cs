using UnityEngine;

namespace GameCamera
{
    public class LevelFocus : MonoBehaviour
    {
        [SerializeField]
        private Transform _levelCenterTransform;

        void Start()
        {
            transform.LookAt(_levelCenterTransform);
        }
    }
}
