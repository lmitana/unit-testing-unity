using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayModeTests {

    [Test]
    [UnityPlatform(RuntimePlatform.Android)]
    public void RunPlayModeAndroidTestOne() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    [UnityPlatform(RuntimePlatform.Android)]
    public IEnumerator RunPlayModeAndroidTestTwo() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
