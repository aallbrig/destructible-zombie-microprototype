using System;

namespace Models
{
    public interface IZombiePart
    {
        public event Action OnKilled;
    }
}