using UnityEngine;

namespace AngryBirds3D.Slingshot
{
    public struct ShotData
    {
        // throwable data
        public float ThrowableMass;
        public float TrowableDrag;
        public Vector3 ThrowablePosition;

        // slingshot data
        public Vector3 ShotDirection;
        public float ShotForce;

        public ShotData(
            float throwableMass, 
            float trowableDrag, 
            Vector3 throwablePosition, 
            Vector3 shotDirection, 
            float shotForce)
        {
            ThrowableMass = throwableMass;
            TrowableDrag = trowableDrag;
            ThrowablePosition = throwablePosition;
            ShotDirection = shotDirection;
            ShotForce = shotForce;
        }
    }
}
