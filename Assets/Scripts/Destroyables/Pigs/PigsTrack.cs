using System;
using System.Collections.Generic;
using UnityEngine;

namespace AngryBirds3D.Destroyables.Pigs
{
	public class PigsTrack : MonoBehaviour
	{
		private List<GameObject> _pigs = new List<GameObject>();

		public event Action AllPigsDefeatedEvent;

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
				pig.GetComponent<Pig>().PigDefeatedEvent += PigDefeated;
			}
		}

		private void PigDefeated(GameObject pig)
		{
			_pigs.Remove(pig);

			// What is that? You really need this mess here ???
			if (PigsCount() == 0)
			{
				AllPigsDefeatedEvent?.Invoke();
			}
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
