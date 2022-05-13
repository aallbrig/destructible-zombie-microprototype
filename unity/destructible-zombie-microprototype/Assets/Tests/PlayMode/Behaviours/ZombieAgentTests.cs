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
            var testZombiePart = new GameObject().AddComponent<ZombiePartSpy>();
            var sut = new GameObject().AddComponent<ZombieAgent>();
            var eventCallCount = 0;
            sut.ZombiePartRegistered += _ => eventCallCount++;
            testZombiePart.transform.parent = sut.transform;
            yield return null;

            Assert.AreEqual(1, eventCallCount);
        }

        [UnityTest]
        public IEnumerator ZombieAgent_CanBeMadeUpOfNestedZombieParts()
        {
            var testZombiePart1 = new GameObject().AddComponent<ZombiePartSpy>();
            var testZombiePart2 = new GameObject().AddComponent<ZombiePartSpy>();
            var sut = new GameObject().AddComponent<ZombieAgent>();
            var eventCallCount = 0;
            sut.ZombiePartRegistered += _ => eventCallCount++;
            testZombiePart2.transform.parent = testZombiePart1.transform;
            testZombiePart1.transform.parent = sut.transform;
            yield return null;

            Assert.AreEqual(2, eventCallCount);
        }
    }
}