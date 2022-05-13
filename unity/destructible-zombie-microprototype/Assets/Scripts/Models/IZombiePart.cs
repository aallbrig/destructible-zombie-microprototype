using System;

namespace Models
{
    public interface IZombiePart
    {
        public event Action ZombiePartKilled;
        public void Kill();
    }
}