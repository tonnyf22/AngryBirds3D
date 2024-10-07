using System.Collections.Generic;
using UnityEngine;

namespace AngryBirds3D.Throwables.Birds
{
    public class BirdsTrack : MonoBehaviour
    {
        private List<GameObject> _birds = new List<GameObject>();

        void Awake()
        {
            SeekForLastChildren(transform);
        }

        private void SeekForLastChildren(Transform parent)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Transform childTransform = parent.transform.GetChild(i);
                // if (childTransform.childCount > 0)
                // {
                //     SeekForLastChildren(childTransform);
                // }
                // else
                // {
                    _birds.Add(childTransform.gameObject);
                // }
            }
        }

        public bool IsHaveBirds()
        {
            return _birds.Count > 0;
        }

        public int BirdsCount()
        {
            return _birds.Count;
        }

        public GameObject GetNextBird()
        {
            if (IsHaveBirds())
            {
                GameObject bird = _birds[0];

                _birds.RemoveAt(0);
				// bird.transform.parent = null;

                return bird;
            }
            else
            {
                throw new NoBirdsException();
            }
        }
    }
}
