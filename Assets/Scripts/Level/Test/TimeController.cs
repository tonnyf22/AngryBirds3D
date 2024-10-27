using UnityEngine;

namespace AngryBirds3D.Level.Test
{
    public class TimeController : MonoBehaviour
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
