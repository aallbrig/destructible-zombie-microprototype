using System.Collections;
using Controllers;
using NUnit.Framework;
using Tests.Utilities.Spies;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;

namespace Tests.PlayMode.Controllers
{
    public class PlayerControllerTests : InputTestFixture
    {
        [UnityTest]
        public IEnumerator PlayerController_CanTapToInteract()
        {
            InputSystem.AddDevice<Touchscreen>();
            var pointer = InputSystem.AddDevice<Pointer>();
            var sut = new GameObject().AddComponent<PlayerController>();
            var eventCalled = false;
            var testCamera = new GameObject { transform = { position = new Vector3(10, 1, -10) } }.AddComponent<Camera>();
            var testInteractable = new GameObject { transform = { position = new Vector3(10, 1, 10) } }.AddComponent<SpyInteractable>();
            testInteractable.Interacted += () => eventCalled = true;
            sut.perspectiveCamera = testCamera;
            yield return new WaitForFixedUpdate();

            var worldSpaceToScreen = testCamera.WorldToScreenPoint(testInteractable.transform.position);
            BeginTouch(pointer.deviceId, worldSpaceToScreen);
            EndTouch(pointer.deviceId, worldSpaceToScreen);

            Assert.IsTrue(eventCalled);
            Assert.IsTrue(testInteractable.interactedWith);
        }
    }
}