using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Linq;

public class EditorTests {

    [Test]
    [Combinatorial]
    [Category("My Category")]
    [Author("Alexandru Dima", "alex@dima.to")]
    [Apartment(System.Threading.ApartmentState.STA)]
    [Description("This is a test.")]
    public void CombinatorialTest([Values(1, 2)] int a, [Values("A", "B")] string b, [Values(true, false)] bool c)
    {
        Debug.LogWarningFormat("CombinatorialTest {0}, {1}, {2}", a, b, c);
    }

    [Test]
    [Pairwise]
    public void PairwiseTest([Values(1, 2)] int a, [Values("A", "B")] string b, [Values(true, false)] bool c)
    {
        Debug.LogWarningFormat("PairwiseTest {0}, {1}, {2}", a, b, c);
    }

    [Test]
    [Sequential]
    public void SequentialTest([Values(1, 2)] int a, [Values("A", "B")] string b, [Values(true, false)] bool c)
    {
        Debug.LogWarningFormat("SequentialTest {0}, {1}, {2}", a, b, c);
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Debug.LogWarning("OneTimeSetUp was called.");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Debug.LogWarning("OneTimeTearDown was called.");
    }

    [Test]
    public void IgnoreLogErrors()
    {
        LogAssert.ignoreFailingMessages = true;
        Debug.LogError("Whoops.");
        Debug.LogError("Whoops.");
        Debug.LogError("Whoops.");
        LogAssert.ignoreFailingMessages = false;
    }

    [Test]
    [Order(3)]
    public void TestA()
    {
        Debug.LogWarning("Order 3");
    }

    [Test]
    [Order(2)]
    public void TestB()
    {
        Debug.LogWarning("Order 2");
    }

    [Test]
    [Order(1)]
    public void TestC()
    {
        Debug.LogWarning("Order 1");
    }
    
    [Test]
    [Ignore("This test is ignored.")]
    public void IgnoredTest()
    {

    }

    [Test]
    [UnityPlatform(exclude = new RuntimePlatform[] { }, include = new RuntimePlatform[] { RuntimePlatform.WindowsEditor, RuntimePlatform.WindowsPlayer })]
    public void IncludedWindowsPlatformTest()
    {

    }

    [Test]
    [UnityPlatform(exclude = new RuntimePlatform[] { RuntimePlatform.WindowsEditor, RuntimePlatform.WindowsPlayer }, include = new RuntimePlatform[] { })]
    public void ExcludedWindowsPlatformTest()
    {

    }

#if UNITY_STANDALONE_WIN
    [Test]
    public void WindowsTest()
    {

    }
#endif

#if UNITY_IPHONE
    [Test]
    public void iPhoneTest()
    {

    }
#endif

    [Test]
    [Repeat(3)]
    public void RepeatedTest()
    {
        Debug.LogWarningFormat("RepeatedTest");
    }

    private static int retryIteration = 0;

    [Test]
    [Retry(3)]
    public void RetryTest()
    {
        Debug.LogWarning("RetryTest was called.");
        retryIteration++;
        if (retryIteration < 3)
            Assert.That(false);
        else
            Assert.That(true);
    }
    
    [Test]
    public void Random([Random(0, 10, 5)] int x)
    {
        Debug.LogWarningFormat("Random {0}", x);
    }

    [Test]
    public void Range([NUnit.Framework.Range(1, 10, 1)] int x)
    {
        Debug.LogWarningFormat("Range {0}", x);
    }

    [TestCase(10, 5, 2)]
    [TestCase(5, 5, 1)]
    [TestCase(1, 1, 1)]
    public void DivisionTests(int a, int b, int c)
    {
        Debug.LogWarningFormat("Testing whether {0} / {1} = {2}", a, b, c);
        Assert.AreEqual(a / b, c);
    }

    [TestCaseSource("DivisionTests2Source")]
    public void DivisionTests2(int a, int b, int c)
    {
        Debug.LogWarningFormat("Testing whether {0} / {1} = {2}", a, b, c);
        Assert.AreEqual(a / b, c);
    }

    static object[] DivisionTests2Source =
    {
        new object[] { 10, 5, 2 },
        new object[] { 5, 5, 1 },
        new object[] { 1, 1, 1 }
    };

    [DatapointSource]
    public float[] values = new float[] { -1f, 0f, 1f, 11f };

    [Theory]
    public void SquareRootDefinition(float value)
    {
        Assume.That(value >= 0f);
        Debug.LogWarningFormat("SquareRootDefinition: {0}", value);
        var root = Mathf.Sqrt(value);
        Assert.That(root >= 0f);
        Assert.That(root * root, Is.EqualTo(value).Within(0.000001f));
    }
    
    public static float[] values2 = new float[] { 0f, 1f, 11f };

    [Test]
    public void SquareRootDefinition2([ValueSource("values2")] float value)
    {
        Debug.LogWarningFormat("SquareRootDefinition2: {0}", value);
        var root = Mathf.Sqrt(value);
        Assert.That(root >= 0f);
        Assert.That(root * root, Is.EqualTo(value).Within(0.000001f));
    }

    [Test]
    [MaxTime(3000)]
    [Timeout(3000)] // Timeout only seems to work in play mode tests.
    public void MaxTimeTest()
    {
        System.Threading.Thread.Sleep(2000);
    }

    public enum TestSeverityLevel
    {
        Minor,
        Major
    }

    public class TestSeverityAttribute : NUnit.Framework.PropertyAttribute
    {
        public TestSeverityAttribute(TestSeverityLevel level) : base()
        {
            Debug.LogWarningFormat("Test severity: {0}", level);
        }
    }

    [Test]
    [TestSeverity(TestSeverityLevel.Minor)]
    public void CustomPropertyTest()
    {

    }

    [UnityTest]
    public IEnumerator TestCharacterInventoryMonoBehaviour()
    {
        LogAssert.ignoreFailingMessages = true;
        var inventoryDisplayHolder = new GameObject();
        for (int i = 0; i < 100; i++)
        {
            var imageObject = new GameObject();
            var imageComponent = imageObject.AddComponent<UnityEngine.UI.Image>();
            imageObject.transform.SetParent(inventoryDisplayHolder.transform);
            var textObject = new GameObject();
            var text = textObject.AddComponent<UnityEngine.UI.Text>();
            text.text = "Test";
            text.transform.SetParent(imageComponent.transform);
        }
        var player = new GameObject();
        player.tag = "Player";
        var characterStats = player.AddComponent<CharacterStats>();
        characterStats.characterDefinition = new CharacterStats_SO
        {
            currentEncumbrance = 10f,
            maxEncumbrance = 50f
        };
        var inventory = new CharacterInventory
        {
            InventoryDisplayHolder = inventoryDisplayHolder,
            hotBarDisplayHolders = inventoryDisplayHolder.GetComponentsInChildren<UnityEngine.UI.Image>().Take(4).ToArray()
        };
        inventory.Start();
        var frameCount = 0;
        while ((frameCount++) <= 20)
        {
            var itemGameObject = new GameObject();
            var itemPickup = itemGameObject.AddComponent<ItemPickUp>();
            itemPickup.itemDefinition = new ItemPickUps_SO
            {
                itemWeight = 1f
            };
            inventory.StoreItem(itemPickup);
            inventory.Update();
            yield return null;
        }
        LogAssert.ignoreFailingMessages = false;
        LogAssert.Expect(LogType.Log, "Inventory is Full");
    }
}
