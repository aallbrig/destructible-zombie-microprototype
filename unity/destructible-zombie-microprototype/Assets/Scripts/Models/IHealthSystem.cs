using System;

namespace Models
{
    public interface IHealthSystem
    {
        public event Action<float> Damaged;
        public event Action<float> Healed;
        public event Action Killed;

        public void Hurt(float value);
        public void Reset();
    }
}