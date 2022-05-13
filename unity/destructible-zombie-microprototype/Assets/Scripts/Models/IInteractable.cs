using System;

namespace Models
{
    public interface IInteractable
    {
        public event Action Interacted;

        public void Interact();
    }
}