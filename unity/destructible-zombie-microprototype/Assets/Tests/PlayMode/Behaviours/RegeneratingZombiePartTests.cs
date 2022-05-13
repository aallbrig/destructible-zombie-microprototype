using System.Collections;
using Behaviours;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace Tests.PlayMode.Behaviours
{
    public class RegeneratingZombiePartTests
    {
        [UnityTest]
        public IEnumerator RegeneratingZombiePart_RegeneratesAfterBeingKilled()
        {
            var sut = new GameObject().AddComponent<RegeneratingZombiePart>();
            var eventCalled = false;
            sut.ZombiePartRegenerated += () => eventCalled = true;
            sut.waitTimeBeforeRegenerateInSeconds = 0.1f;
            yield return null;

            sut.Kill();
            
            yield return new WaitForSeconds(0.11f);

            Assert.IsTrue(eventCalled);
        }

        [UnityTest]
        public IEnumerator RegeneratingZombiePart_AllowsDesignersToAttachUnityEventsToOnKilled()
        {
            var sut = new GameObject().AddComponent<RegeneratingZombiePart>();
            var eventCalled = false;
            var testUnityEvent = new UnityEvent();
            testUnityEvent.AddListener(() => eventCalled = true);
            sut.onKilled.Add(testUnityEvent);
            yield return null;

            sut.Kill();

            Assert.IsTrue(eventCalled);
        }

        [UnityTest]
        public IEnumerator RegeneratingZombiePart_AllowsDesignersToAttachUnityEventsToOnRegenerate()
        {
            var sut = new GameObject().AddComponent<RegeneratingZombiePart>();
            var eventCalled = false;
            var testUnityEvent = new UnityEvent();
            testUnityEvent.AddListener(() => eventCalled = true);
            sut.onRegenerate.Add(testUnityEvent);
            yield return null;

            sut.Kill();
            sut.Regenerate();

            Assert.IsTrue(eventCalled);
        }
    }
}