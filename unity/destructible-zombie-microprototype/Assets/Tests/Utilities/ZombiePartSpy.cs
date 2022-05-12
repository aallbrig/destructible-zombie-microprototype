using System;
using Models;
using UnityEngine;

namespace Tests.Utilities
{
    public class ZombiePartSpy : MonoBehaviour, IZombiePart
    {
        public event Action OnKilled;

        public void Kill()
        {
            OnKilled?.Invoke();
        }
    }
}
