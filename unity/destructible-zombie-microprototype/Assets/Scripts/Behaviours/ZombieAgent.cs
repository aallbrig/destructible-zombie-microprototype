using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Behaviours
{
    public class ZombieAgent : UnityEngine.MonoBehaviour
    {
        public event Action<IZombiePart> ZombiePartRegistered;
        private List<IZombiePart> _zombieParts = new List<IZombiePart>();
        private void Start()
        {
            DiscoverAndSetZombieParts();
        }
        private void DiscoverAndSetZombieParts()
        {
            Reset();
            ScanForZombieParts().ForEach(zombiePart =>
            {
                ZombiePartRegistered?.Invoke(zombiePart);
                _zombieParts.Add(zombiePart);
            });
            _zombieParts.AddRange(ScanForZombieParts());
            BindListeners();
        }
        private void BindListeners()
        {
            _zombieParts.ForEach(zombiePart => {});
        }
        private void Reset()
        {
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