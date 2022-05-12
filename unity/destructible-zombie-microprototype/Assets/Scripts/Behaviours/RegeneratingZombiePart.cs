using System;
using Models;
using UnityEngine;

namespace Behaviours
{
    public class RegeneratingZombiePart : MonoBehaviour, IZombiePart
    {
        public event Action OnKilled;
    }
}