using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using ScriptableObjects;
using UnityEditorInternal;
using UnityEngine;

namespace Behaviours
{
    public class ZombieAgent : MonoBehaviour
    {
        public event Action<IZombiePart> ZombiePartDiscovered;
        public event Action<ZombieAgent> ZombieAgentKilled;
        public ZombieHealth zombieHealthSystem;

        private readonly List<IZombiePart> _zombieParts = new List<IZombiePart>();
        public IHealthSystem HealthSystem;

        private void Start()
        {
            HealthSystem ??= zombieHealthSystem != null ? (IHealthSystem)Instantiate(zombieHealthSystem) : new HealthSystem();

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
            HealthSystem.Killed += () => ZombieAgentKilled?.Invoke(this);
            _zombieParts.ForEach(zombiePart => zombiePart.ZombiePartKilled += OnZombiePartKilled);
        }
        private void OnZombiePartKilled() => HealthSystem.Hurt(1);
        private void Reset()
        {
            HealthSystem.Reset();
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