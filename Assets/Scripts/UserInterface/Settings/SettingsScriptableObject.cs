using UnityEngine;

namespace AngryBirds3D.UserInterface.Settings
{
    [CreateAssetMenu(
        fileName = "Settings",
        menuName = "ScriptableObjects/SettingsScriptableObject",
        order = 2)]
    public class SettingsScriptableObject : ScriptableObject
    {
        [Range(-80.0f, 0.0f)]
        public float VolumeSFX;

        [Range(-80.0f, 0.0f)]
        public float VolumeMusic;
    }
}
