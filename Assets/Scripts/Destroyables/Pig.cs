using System;

namespace AngryBirds3D.Destroyables
{
    public class Pig : Destroyable
    {
        public event Action PigDefeatedEvent;

        protected override void ObjectDestroyedSpecificLogic()
        {
            PigDefeatedEvent?.Invoke();
        }
    }
}
