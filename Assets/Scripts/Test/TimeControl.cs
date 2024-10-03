using UnityEngine;

namespace AngryBirds3D.Test
{
    public class TimeControl : MonoBehaviour
    {
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float _timeScale;

        void Update()
        {
            Time.timeScale = _timeScale;
        }
    }
}
