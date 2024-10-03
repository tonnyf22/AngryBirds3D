using System;

namespace AngryBirds3D.Destroyables
{
    public class Fortification : Destroyable
    {
        public event Action FortificationDestroyedEvent;

        protected override void ObjectDestroyedSpecificLogic()
        {
            FortificationDestroyedEvent?.Invoke();
        }
    }
}
