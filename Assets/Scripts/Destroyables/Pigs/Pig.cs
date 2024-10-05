using System;
using UnityEngine;

namespace AngryBirds3D.Destroyables.Pigs
{
    public class Pig : Destroyable
    {
        public event Action<GameObject> PigDefeatedEvent;

        protected override void ObjectDestroyedSpecificLogic()
        {
            PigDefeatedEvent?.Invoke(gameObject);
        }
    }
}
