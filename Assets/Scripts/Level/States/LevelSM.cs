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

        private MonoBehaviour _currentState;
        public MonoBehaviour CurrentState { get { return _currentState; } }

        void Start()
        {
            _passCheckState.enabled = true;
        }

        public void ChangeState(MonoBehaviour fromState, MonoBehaviour toState)
        {
            toState.enabled = true;

            _currentState = toState;

            fromState.enabled = false;
        }

        public bool IsSlingshotAimStateEnabled()
        {
            return _slingshotAimState.enabled;
        }
    }
}
