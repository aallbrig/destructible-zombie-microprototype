using System;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.Events;

namespace Behaviours
{
    public class RegeneratingZombiePart : MonoBehaviour, IZombiePart
    {
        public event Action ZombiePartKilled;
        public event Action ZombiePartRegenerated;
        public List<UnityEvent> onKilled = new List<UnityEvent>();
        public List<UnityEvent> onRegenerate = new List<UnityEvent>();
        public float waitTimeBeforeRegenerateInSeconds = 1f;
        private float _timeKilled;
        private bool _dead;

        private void Start()
        {
            ZombiePartKilled += () => onKilled.ForEach(unityEvent => unityEvent.Invoke());
            ZombiePartRegenerated += () => onRegenerate.ForEach(unityEvent => unityEvent.Invoke());
        }

        public void Kill()
        {
            // Cannot be killed if already dead
            if (_dead == true) return;

            _timeKilled = Time.time;
            _dead = true;
            ZombiePartKilled?.Invoke();
        }

        private void Update()
        {
            TryRegenerate();
        }
        private void TryRegenerate()
        {
            // should not regenerate if already alive
            if (IsAlive()) return;

            if (IsTimeToRegenerate()) Regenerate();
        }
        private bool IsTimeToRegenerate() => Time.time - _timeKilled > waitTimeBeforeRegenerateInSeconds;
        private bool IsAlive() => _dead == false;

        public void Regenerate()
        {
            // should not regenerate if already alive
            if (IsAlive()) return;

            _dead = false;
            ZombiePartRegenerated?.Invoke();
        }
    }
}