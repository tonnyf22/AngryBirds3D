using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slingshot
{
	public class TrajectoryPrediction : MonoBehaviour
	{
		private const int PREWARMED_DOTS = 50;
		private const int MAX_DOTS = 200;

		[SerializeField]
		[Range(0.05f, 0.5f)]
		private float _trajectoryDotSetDeltaTime;

		[SerializeField]
		private GameObject _trajectoryDotPrefab;
		private List<GameObject> _trajectoryDots = null;

		[SerializeField]
		[Range(0.05f, 2.0f)]
		private float _collisionCheckExtraPercent;

		[SerializeField]
		private GameObject _hitMarkPrefab;
		private GameObject _hitMark = null;

		void Awake()
		{
			PreinstantiateFewHiddenDots();
			PreinstantiateHiddenHitMark();
		}

		private void PreinstantiateFewHiddenDots()
		{
			_trajectoryDots = new List<GameObject>();
			for (int i = 0; i < PREWARMED_DOTS; i++)
			{
				InstantiateOneMoreHiddenDot();
			}
		}
	
		private void InstantiateOneMoreHiddenDot()
		{
			GameObject dot =
				Instantiate(_trajectoryDotPrefab);
			dot.SetActive(false);

			_trajectoryDots.Add(dot);
		}

		private void PreinstantiateHiddenHitMark()
		{
			GameObject hitMark =
				Instantiate(_hitMarkPrefab);
			hitMark.SetActive(false);
			_hitMark = hitMark;
		}

		public void CreateShotTrajectory(ShotData pd)
		{
			int lastUsedDotIndex = 0;

			Vector3 v0 = pd.ShotDirection * (pd.ShotForce / pd.ThrowableMass);
			Vector3 p0 = pd.ThrowablePosition;
			SetTrajectoryDotPositionAndRevealIt(0, p0);

			Vector3 v = v0;
			Vector3 p = p0;
			Vector3 pNext;
			bool isHitGround = false;
			bool isCollisionDetected = false;
			for (int dotIndex = 1; dotIndex < MAX_DOTS && !isHitGround; dotIndex++)
			{
				v = CalculateNextVelocity(
						v, 
						pd.TrowableDrag,
						_trajectoryDotSetDeltaTime);
				pNext = 
					CalculateNextPosition(
						p,
						v,
						_trajectoryDotSetDeltaTime
					);

				float collisionCheckDistance = 
					Vector3.Distance(p, pNext) * 
					(1.0f + _collisionCheckExtraPercent);
				isCollisionDetected = 
					IsCollisionDetected(
						p,
						v.normalized,
						out RaycastHit hit,
						collisionCheckDistance
					);
				if (isCollisionDetected)
				{
					// SetTrajectoryDotPositionAndRevealIt(dotIndex, hit.point);
					SetHitMarkerPositionAndRevealIt(hit.point);
					
					isHitGround = true;
				}
				else
				{
					p = pNext;
					SetTrajectoryDotPositionAndRevealIt(dotIndex, p);

                    lastUsedDotIndex = dotIndex;
				}
			}

			HideUnusedDotsStartingFrom(lastUsedDotIndex);
			if (!isCollisionDetected)
			{
				HideHitMark();
			}
		}

        private void SetTrajectoryDotPositionAndRevealIt(
			int dotIndex, 
			Vector3 dotPosition)
		{
			if (dotIndex > _trajectoryDots.Count - 1)
			{
				InstantiateOneMoreHiddenDot();
			}

			_trajectoryDots[dotIndex].transform.position = dotPosition;
			_trajectoryDots[dotIndex].SetActive(true);
		}

        private Vector3 CalculateNextVelocity(
			Vector3 velocity,
			float drag,
			float timeInterval)
        {
			Vector3 velocityNext =
				(velocity + Physics.gravity * timeInterval) *
				Mathf.Clamp01(1 - drag * timeInterval);
			return velocityNext;
        }

        private Vector3 CalculateNextPosition(
			Vector3 position,
			Vector3 velocity,
			float timeInterval
		)
        {
            Vector3 positionNext = 
				position + velocity * timeInterval;
			return positionNext;
        }

        private bool IsCollisionDetected(
			Vector3 position,
			Vector3 direction,
			out RaycastHit hit,
			float distance)
		{
			return 
				Physics.Raycast(
					position, 
					direction, 
					out hit, 
					distance);
		}

		private void SetHitMarkerPositionAndRevealIt(Vector3 position)
		{
			_hitMark.transform.position = position;
			_hitMark.SetActive(true);
		}

        private void HideUnusedDotsStartingFrom(int lastUsedDotIndex)
        {
			bool isLastDotActive = true;
			for (
				int dotIndex = lastUsedDotIndex + 1; 
				dotIndex < _trajectoryDots.Count && isLastDotActive; 
				dotIndex++)
			{
				isLastDotActive = _trajectoryDots[dotIndex].activeInHierarchy;
				_trajectoryDots[dotIndex].SetActive(false);
			}
        }

        private void HideHitMark()
        {
			if (_hitMark.activeInHierarchy)
			{
				_hitMark.SetActive(false);
			}
        }
	}
}
