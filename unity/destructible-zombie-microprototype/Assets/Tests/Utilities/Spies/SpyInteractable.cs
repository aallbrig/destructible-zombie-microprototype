using System;
using Models;
using UnityEngine;

namespace Tests.Utilities.Spies
{
    [RequireComponent(typeof(BoxCollider))]
    public class SpyInteractable : MonoBehaviour, IInteractable
    {
        public event Action Interacted;

        public bool interactedWith = false;

        public void Interact()
        {
            interactedWith = true;
            Interacted?.Invoke();
        }
    }
}