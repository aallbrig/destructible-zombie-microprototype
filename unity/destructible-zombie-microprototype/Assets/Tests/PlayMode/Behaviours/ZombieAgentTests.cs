using System.Collections;
using Behaviours;
using NUnit.Framework;
using Tests.Utilities;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.Behaviours
{
    public class ZombieAgentTests
    {
        [UnityTest]
        public IEnumerator ZombieAgent_CanBeMadeUpOfAZombiePart()
        {
            var sut = new GameObject().AddComponent<ZombieAgent>();
            var testZombiePart = new GameObject().AddComponent<ZombiePartSpy>();
            var eventCallCount = 0;
            sut.ZombiePartDiscovered += _ => eventCallCount++;
            testZombiePart.transform.parent = sut.transform;
            yield return null;

            Assert.AreEqual(1, eventCallCount);
        }

        [UnityTest]
        public IEnumerator ZombieAgent_CanBeMadeUpOfNestedZombieParts()
        {
            var sut = new GameObject().AddComponent<ZombieAgent>();
            var testZombiePart1 = new GameObject().AddComponent<ZombiePartSpy>();
            var testZombiePart2 = new GameObject().AddComponent<ZombiePartSpy>();
            var eventCallCount = 0;
            sut.ZombiePartDiscovered += _ => eventCallCount++;
            testZombiePart2.transform.parent = testZombiePart1.transform;
            testZombiePart1.transform.parent = sut.transform;
            yield return null;

            Assert.AreEqual(2, eventCallCount);
        }

        [UnityTest]
        public IEnumerator ZombieAgent_Dies_WhenEnoughZombieParts_AreKilled()
        {
            var sut = new GameObject().AddComponent<ZombieAgent>();
            var eventCalled = false;
            var testZombiePart = new GameObject().AddComponent<ZombiePartSpy>();
            sut.ZombieAgentKilled += _ => eventCalled = true;
            sut.maxHealth = 1;
            testZombiePart.transform.parent = sut.transform;
            yield return null;

            testZombiePart.Kill();

            Assert.IsTrue(eventCalled);
        }
    }
}