using System;
using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new Zombie Health", menuName = "GAME/ZombieHealth", order = 0)]
    // Decorator wrapping a IHealthSystem
    public class ZombieHealth : ScriptableObject, IHealthSystem
    {
        public event Action<float> Damaged;
        public event Action<float> Healed;
        public event Action Killed;
        public float maxHealth = 3;

        private IHealthSystem _healthSystem;

        private void OnEnable()
        {
            _healthSystem ??= new HealthSystem { MaxHealth = maxHealth };
            _healthSystem.Damaged += value => Damaged?.Invoke(value);
            _healthSystem.Healed += value => Healed?.Invoke(value);
            _healthSystem.Killed += () => Killed?.Invoke();
        }
        public void Hurt(float value) => _healthSystem.Hurt(value);
        public void Reset() => _healthSystem.Reset();
    }
}