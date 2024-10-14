using UnityEngine;

namespace AngryBirds3D.Level.States
{
    public class LevelSM : MonoBehaviour
    {
        [SerializeField]
        private PassCheckState _passCheckState;
        [SerializeField]
        private BirdLoadState _birdLoadState;
        [SerializeField]
        private SlingshotAimState _slingshotAimState;
        [SerializeField]
        private BirdFlyState _birdFlyState;
        [SerializeField]
        private BirdHitState _birdHitState;
        [SerializeField]
        private CompletionState _completionState;

        void Start()
        {
            _passCheckState.enabled = true;
        }

        public void ChangeState(MonoBehaviour fromState, MonoBehaviour toState)
        {
            toState.enabled = true;
            fromState.enabled = false;
        }
    }
}
