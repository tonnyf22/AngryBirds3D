using UnityEngine;

namespace AngryBirds3D.Slingshot
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField]
        private SlingshotSpringInput _slingshotSpringInput;

        [SerializeField]
        [Range(0.0f, 45.0f)]
        private float _maxRotationAngleLeft;
        [SerializeField]
        [Range(0.0f, 45.0f)]
        private float _maxRotationAngleRight;

        void OnEnable()
        {
            _slingshotSpringInput.RotateSlingshotEvent += RotateSlingshot;
        }

        void OnDisable()
        {
            _slingshotSpringInput.RotateSlingshotEvent -= RotateSlingshot;
        }

        private void RotateSlingshot(float rotation)
        {
            float finalRotatoin = transform.eulerAngles.y + rotation;
            if (finalRotatoin < 360.0f - _maxRotationAngleLeft && finalRotatoin >= 180.0f)
            {
                finalRotatoin = 360.0f - _maxRotationAngleLeft;
            }
            else if (finalRotatoin > _maxRotationAngleRight && finalRotatoin < 180.0f)
            {
                finalRotatoin = _maxRotationAngleRight;
            }
            
            transform.rotation = 
                Quaternion.Euler(
                    transform.eulerAngles.x,
                    finalRotatoin,
                    transform.eulerAngles.z
                );
        }
    }
}
