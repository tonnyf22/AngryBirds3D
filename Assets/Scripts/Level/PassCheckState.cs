using AngryBirds3D.Destroyables.Pigs;
using AngryBirds3D.Throwables.Birds;
using UnityEngine;

namespace AngryBirds3D.Level
{
    public class PassCheckState : MonoBehaviour
    {
        [SerializeField]
        private LevelSM _levelSM;

        [SerializeField]
        private BirdLoadState _birdLoadState;
        [SerializeField]
        private CompletionState _completionState;

        [SerializeField]
        private PigsTrack _pigsTrack;
        [SerializeField]
        private BirdsTrack _birdsTrack;

        void OnEnable()
        {
            // enable necessary scripts on objects
            // don't really need any specific "per-state" functionality

            DetermineNextState();
        }

        private void DetermineNextState()
        {
            if (IsLevelEdgeStateNotReached())
            {
				_levelSM.ChangeState(this, _birdLoadState);
				Debug.Log("load new");
            }
            else
            {
                _levelSM.ChangeState(this, _completionState);
				Debug.Log("over");
            }
        }

		private bool IsLevelEdgeStateNotReached()
		{
			Debug.Log("pigs: " + _pigsTrack.PigsCount() + "; birds: " + _birdsTrack.BirdsCount());
			return _pigsTrack.PigsCount() > 0 && _birdsTrack.BirdsCount() > 0;
		}

        void OnDisable()
        {
            // no specific functionality was enabled, so no need to disable anything
        }
    }
}
