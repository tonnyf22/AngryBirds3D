using UnityEngine;

namespace AngryBirds3D.Level
{
    public class CompletionState : MonoBehaviour
    {
        [SerializeField]
        private LevelSM _levelSM;


        void OnEnable()
        {
            Debug.Log("Results State");
        }

        void OnDisable()
        {

        }
    }
}
