using System;
using System.Collections.Generic;
using UnityEngine;

namespace AngryBirds3D.Destroyables.Pigs
{
	public class PigsTrack : MonoBehaviour
	{
		private List<GameObject> _pigs = new List<GameObject>();

		public event Action AllPigsDefeatedEvent;
		public event Action<int> ScoreEvent;

		public event Action<GameObject> PigDefeatedEvent;

		void Awake()
		{
			SeekForLastChildren(transform);
			ServePigsDefeat();
		}

		private void SeekForLastChildren(Transform parent)
		{
			for (int i = 0; i < parent.transform.childCount; i++)
			{
				Transform childTransform = parent.transform.GetChild(i);

                _pigs.Add(childTransform.gameObject);
			}
		}

		private void ServePigsDefeat()
		{
			foreach (var pig in _pigs)
			{
				Pig pigLogic = pig.GetComponent<Pig>();

				pigLogic.ScoreEvent += Score;
				pigLogic.PigInstanceDefeatedEvent += PigDefeated;
			}
		}

		private void PigDefeated(GameObject pig)
		{
			PigDefeatedEvent?.Invoke(pig);

			_pigs.Remove(pig);

			if (PigsCount() == 0)
			{
				AllPigsDefeatedEvent?.Invoke();
			}
		}

		private void Score(int score)
		{
			ScoreEvent?.Invoke(score);
		}

		public bool IsHavePigs()
		{
			return _pigs.Count > 0;
		}

		public int PigsCount()
		{
			return _pigs.Count;
		}
	}
}
