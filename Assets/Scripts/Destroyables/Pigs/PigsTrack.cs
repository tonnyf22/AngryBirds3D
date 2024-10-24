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
				// if (childTransform.childCount > 0)
				// {
				// 	SeekForLastChildren(childTransform);
				// }
				// else
				// {
					_pigs.Add(childTransform.gameObject);
				// }
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
			// wow, accidentally got big brain move here:
			// like every pig's defeat will concentrate on this particular event
			// like reinvoking it from one class
			// Is it coolness or stupidity ??? The last one pbly...
			PigDefeatedEvent?.Invoke(pig);

			_pigs.Remove(pig);

			// What is that? You really need this mess here ???
			// Yeah, I DO need. I have even subscribed to this event already B)
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
