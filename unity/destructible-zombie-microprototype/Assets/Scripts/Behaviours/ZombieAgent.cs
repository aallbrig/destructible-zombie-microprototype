using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

namespace Behaviours
{
    public class ZombieAgent : MonoBehaviour
    {
        public event Action<IZombiePart> ZombiePartDiscovered;
        public event Action<ZombieAgent> ZombieAgentKilled;
        public float maxHealth = 3;

        private readonly List<IZombiePart> _zombieParts = new List<IZombiePart>();
        private float _currentHealth;

        private void Start()
        {
            DiscoverAndSetZombieParts();
        }
        private void DiscoverAndSetZombieParts()
        {
            Reset();
            ScanForZombieParts().ForEach(AddZombiePart);
            BindListeners();
        }
        private void AddZombiePart(IZombiePart zombiePart)
        {
            ZombiePartDiscovered?.Invoke(zombiePart);
            _zombieParts.Add(zombiePart);
        }
        private void BindListeners()
        {
            _zombieParts.ForEach(zombiePart =>
            {
                zombiePart.ZombiePartKilled += OnZombiePartKilled;
            });
        }
        private void OnZombiePartKilled()
        {
            Damage(1);
        }
        private void Damage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, maxHealth);
            if (_currentHealth <= 0f) ZombieAgentKilled?.Invoke(this);
        }
        private void Reset()
        {
            _currentHealth = maxHealth;
            UnbindListeners();
            _zombieParts.Clear();
        }
        private void UnbindListeners()
        {
            _zombieParts.ForEach(zombiePart => {});
        }
        private List<IZombiePart> ScanForZombieParts()
        {
            var zombieParts = gameObject.transform.GetComponentsInChildren<IZombiePart>();
            return zombieParts.ToList();
        }
    }
}