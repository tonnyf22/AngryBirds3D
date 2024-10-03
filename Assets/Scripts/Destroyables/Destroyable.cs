using System;
using Unity.VisualScripting;
using UnityEngine;

namespace AngryBirds3D.Destroyables
{
	public abstract class Destroyable : MonoBehaviour
	{
		public event Action<float> ScoreEvent;

		[SerializeField]
		[Range(10, 10000)]
		private float _pointsForDestroyment;

		[SerializeField]
		[Range(10, 1000)]
		private float _initialHealth;
		private float _currentHealth;

		[SerializeField]
		[Range(1, 100)]
		private float _damagePerImpulseUnit;

		[SerializeField]
		[Tooltip("Minimum impulse magnitude of the collision to cause damage.")]
		[Range(1, 100)]
		private float _impulseMagnitudeThreshold;

		private const float IMPULSE_MAGNITUDE_RATIO = 1f; 

		void Start()
		{
			_currentHealth = _initialHealth;
		}

		void OnCollisionStay(Collision collision)
		{
			CollisionCheck(collision);
		}

		void OnCollisionEnter(Collision collision)
		{
			CollisionCheck(collision);
		}

		private void CollisionCheck(Collision collision)
		{
			float impulseMagnitude = collision.impulse.magnitude;
			// Debug.Log(gameObject.name + " impulse : " + impulseMagnitude);

			if (IsEnoughKickPowerToCauseDamage(impulseMagnitude))
			{
                // Debug.Log(gameObject.name + " before damage : current_health : " + _currentHealth);
				float damage = CalculateDamage(impulseMagnitude);
                // Debug.Log(gameObject.name + " damage : " + damage);
				ApplyDamage(damage);
                // Debug.Log(gameObject.name + " after damage : current_health : " + _currentHealth);
				CheckHealthStatus();
			}
		}

		private bool IsEnoughKickPowerToCauseDamage(float impulseMagnitude)
		{
			return impulseMagnitude > _impulseMagnitudeThreshold;
		}

		private float CalculateDamage(float impulseMagnitude)
		{
			return impulseMagnitude * _damagePerImpulseUnit;
		}

		private void ApplyDamage(float damage)
		{
			_currentHealth =
				Mathf.Clamp(
					_currentHealth - damage,
					0.0f,
					_initialHealth);
		}

		private void CheckHealthStatus()
		{
			if (IsNoHealthLeft())
			{
				ScoreEvent?.Invoke(_pointsForDestroyment);

				ObjectDestroyedSpecificLogic();

				Destroy(gameObject);
			}
		}

		private bool IsNoHealthLeft()
		{
			return _currentHealth == 0.0f;
		}

		protected abstract void ObjectDestroyedSpecificLogic();
	}
}
