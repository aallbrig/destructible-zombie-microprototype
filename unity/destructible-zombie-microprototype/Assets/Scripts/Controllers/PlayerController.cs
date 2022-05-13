using System;
using Generated;
using Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public struct InteractionData
    {
        public Vector2 ScreenPosition;
        public float Timing;
    }

    public class PlayerController : MonoBehaviour
    {
        public Camera perspectiveCamera;
        [SerializeField] private Vector2 pointerBeginScreenInput;
        [SerializeField] private Vector2 pointerEndScreenInput;
        private event Action<InteractionData> TouchStarted;
        private event Action<InteractionData> TouchEnded;
        private Gameplay _gameplayControls;
        private Camera _perspectiveCamera;
        private void Awake() => _gameplayControls = new Gameplay();
        private void OnEnable() => _gameplayControls.Enable();
        private void OnDisable() => _gameplayControls.Disable();
        private void Start()
        {
            _perspectiveCamera ??= perspectiveCamera;
            _perspectiveCamera ??= FindObjectOfType<Camera>();
            _perspectiveCamera ??= new GameObject().AddComponent<Camera>();

            _gameplayControls.Player.Press.started += OnTouchStart;
            _gameplayControls.Player.Press.canceled += OnTouchEnd;
            
            TouchEnded += TryInteractWithInteractables;
        }
        private void OnTouchStart(InputAction.CallbackContext ctx)
        {
            pointerBeginScreenInput = _gameplayControls.Player.Position.ReadValue<Vector2>();
            TouchStarted?.Invoke(new InteractionData { ScreenPosition = pointerBeginScreenInput, Timing = Time.time });
        }
        private void OnTouchEnd(InputAction.CallbackContext ctx)
        {
            pointerEndScreenInput = _gameplayControls.Player.Position.ReadValue<Vector2>();
            TouchEnded?.Invoke(new InteractionData { ScreenPosition = pointerBeginScreenInput, Timing = Time.time });
        }
        private void TryInteractWithInteractables(InteractionData data)
        {
            var ray = _perspectiveCamera.ScreenPointToRay(data.ScreenPosition);
            if (Physics.Raycast(ray, out var hit, 100f))
            {
                var maybeInteractable = hit.collider.transform.GetComponent<IInteractable>();
                if (maybeInteractable != null) maybeInteractable.Interact();
            }
        }
    }
}