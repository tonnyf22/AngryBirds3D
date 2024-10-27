using System.Collections.Generic;
using UnityEngine;

namespace AngryBirds3D.Throwables.Birds
{
    public class BirdsTrack : MonoBehaviour
    {
        private List<GameObject> _birds = new List<GameObject>();

        private GameObject _lastExtractedBird;

        void Awake()
        {
            SeekForLastChildren(transform);
        }

        private void SeekForLastChildren(Transform parent)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Transform childTransform = parent.transform.GetChild(i);

                _birds.Add(childTransform.gameObject);
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

        public int GetScoreForSavedBirdByIndex(int index)
        {
            return 
                _birds[index]
                .GetComponent<BirdAbility>()
                .PointsForSave;
        }

        public GameObject GetNextBird()
        {
            if (IsHaveBirds())
            {
                GameObject bird = _birds[0];

                RememberLastExtractedBird(bird);

                return bird;
            }
            else
            {
                throw new NoBirdsException();
            }
        }

        private void RememberLastExtractedBird(GameObject bird)
        {
            _lastExtractedBird = bird;
        }

        public void ForgetLastExtractedBird()
        {
            _birds.Remove(_lastExtractedBird);
        }
    }
}
