using System;
using Models;
using UnityEngine;

namespace Tests.Utilities.Spies
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
