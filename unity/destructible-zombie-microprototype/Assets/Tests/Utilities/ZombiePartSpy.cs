using System;
using Models;
using UnityEngine;

namespace Tests.Utilities
{
    public class ZombiePartSpy : MonoBehaviour, IZombiePart
    {
        public event Action ZombiePartKilled;

        public void Kill()
        {
            ZombiePartKilled?.Invoke();
        }
    }
}
