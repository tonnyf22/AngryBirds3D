using UnityEngine;

namespace Slingshot
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
    }
}
