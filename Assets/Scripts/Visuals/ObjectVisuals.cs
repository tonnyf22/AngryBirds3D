using UnityEngine;
namespace AngryBirds3D.Visuals
{
    public class ObjectVisuals : MonoBehaviour
    {
        protected void InstantiateAndPlayVisualEffect(ParticleSystem particleSystem)
        {
            transform.parent = null;

            ParticleSystem psInstance = Instantiate(particleSystem, transform);
            psInstance.Play();
            Destroy(psInstance.gameObject, psInstance.main.duration);
        }
    }
}
