﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EditModeTests {

    [Test]
    [UnityPlatform(RuntimePlatform.Android)]
    public void RunEditModeAndroidTestOne() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    [UnityPlatform(RuntimePlatform.Android)]
    public IEnumerator RunEditModeAndroidTestTwo() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
