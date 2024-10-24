using System;
using System.Collections.Generic;
using UnityEngine;

namespace AngryBirds3D.Destroyables.Fortifications
{
    public class FortificationsTrack : MonoBehaviour
    {
		private List<GameObject> _fortifications = new List<GameObject>();

		public event Action<int> ScoreEvent;

		void Awake()
		{
			SeekForLastChildren(transform);
			ServeFortificationsDestroyment();
		}

		private void SeekForLastChildren(Transform parent)
		{
			for (int i = 0; i < parent.transform.childCount; i++)
			{
				Transform childTransform = parent.transform.GetChild(i);

				if (childTransform.gameObject.TryGetComponent<Fortification>(out _))
				{
					_fortifications.Add(childTransform.gameObject);
				}
				else if (childTransform.childCount > 0)
				{
					SeekForLastChildren(childTransform);
				}
			}
		}

		private void ServeFortificationsDestroyment()
		{
			foreach (var fortification in _fortifications)
			{
				Fortification fortificationLogic = 
                    fortification.GetComponent<Fortification>();

				fortificationLogic.ScoreEvent += Score;
			}
		}

		private void Score(int score)
		{
			ScoreEvent?.Invoke(score);
		}
    }
}