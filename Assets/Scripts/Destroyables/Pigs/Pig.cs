using System;
using UnityEngine;

namespace AngryBirds3D.Destroyables.Pigs
{
    public class Pig : Destroyable
    {
        public event Action PigDefeatedEvent;
        public event Action<GameObject> PigInstanceDefeatedEvent;

        protected override void ObjectDestroyedSpecificLogic()
        {
            PigDefeatedEvent?.Invoke();
            PigInstanceDefeatedEvent?.Invoke(gameObject);
        }
    }
}
