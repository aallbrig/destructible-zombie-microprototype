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
        public IEnumerator ZombieAgent_CanBeMadeUpOfZombieParts()
        {
            var testZombiePart = new GameObject().AddComponent<ZombiePartSpy>();
            var sut = new GameObject().AddComponent<ZombieAgent>();
            var eventCallCount = 0;
            sut.ZombiePartRegistered += _ => eventCallCount++;
            testZombiePart.transform.parent = sut.transform;
            yield return null;

            Assert.AreEqual(1, eventCallCount);
        }
    }
}