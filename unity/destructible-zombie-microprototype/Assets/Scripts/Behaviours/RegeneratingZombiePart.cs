using System;
using Models;
using UnityEngine;

namespace Behaviours
{
    public class RegeneratingZombiePart : MonoBehaviour, IZombiePart
    {
        public event Action ZombiePartKilled;
        public event Action ZombiePartRegenerated;

        public void Kill() => ZombiePartKilled?.Invoke();
        public void Regenerate() => ZombiePartRegenerated?.Invoke();
    }
}