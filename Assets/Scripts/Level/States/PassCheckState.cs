using AngryBirds3D.Destroyables.Pigs;
using AngryBirds3D.Throwables.Birds;
using UnityEngine;

namespace AngryBirds3D.Level.States
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
			DetermineNextState();
		}

		private void DetermineNextState()
		{
			if (IsLevelEdgeStateNotReached())
			{
				_levelSM.ChangeState(this, _birdLoadState);
			}
			else
			{
				_levelSM.ChangeState(this, _completionState);
			}
		}

		private bool IsLevelEdgeStateNotReached()
		{
			return _pigsTrack.PigsCount() > 0 && _birdsTrack.BirdsCount() > 0;
		}
	}
}
