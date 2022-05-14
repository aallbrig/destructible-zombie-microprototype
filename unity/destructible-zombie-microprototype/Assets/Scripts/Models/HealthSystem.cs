using System;
using UnityEngine;

namespace Models
{
    public class HealthSystem: IHealthSystem
    {
        public event Action<float> Damaged;
        public event Action<float> Healed;
        public event Action Killed;

        public float MaxHealth = 3;
        private float _currentHealth;

        private void ApplyHealthChanges(float value)
        {
            var oldHealth = _currentHealth;
            var updatedHealth = Mathf.Clamp(_currentHealth - value, 0f, MaxHealth);
            _currentHealth = updatedHealth;

            if (updatedHealth > oldHealth)
            {
                var difference = updatedHealth - oldHealth;
                Healed?.Invoke(difference);
            }
            else if (updatedHealth < oldHealth)
            {
                var difference = oldHealth - updatedHealth;
                Damaged?.Invoke(difference);
            }
            if (_currentHealth <= 0f) Killed?.Invoke();
        }
        public void Hurt(float value) => ApplyHealthChanges(value);
        public void Reset()
        {
            _currentHealth = MaxHealth;
        }
    }
}