using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayModeTests {

    [UnityTest]
    public IEnumerator HeroPrefabTest() {
        var source = new GameObject();
        source.AddComponent<Light>();
        var camera = source.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = Color.white;
        camera.tag = "MainCamera";
        var asset = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/System/Prefabs/Main Character.prefab");
        var hero = GameObject.Instantiate(asset, new Vector3(0, -0.7f, 2), new Quaternion(0, 180, 0, 0));
        var underlyingHero = hero.transform.Find("Hero");
        var animator = underlyingHero.GetComponent<Animator>();
        var i = 5;
        while (i-- > 0)
        {
            animator.Play("anm_DwarfHero_Attack01", 0);
            yield return new WaitForSeconds(1);
        }
        GameObject.Destroy(hero);
        GameObject.Destroy(source);
    }

    [Test]
    public void TestSceneLoading()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator TestArtificialIntelligenceCombat()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        var hero = (GameObject)null;
        yield return new WaitUntil(() => (hero = GameObject.Find("Hero")) != null);
        var navMeshAgent = hero.gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.destination = new Vector3(-19.5f, 0, -2.3f);
        yield return new WaitForSeconds(20);
        Assert.AreEqual(null, GameObject.Find("Hero"));
    }

    GameObject fpsTracker;

    [UnityTest]
    public IEnumerator TestFramesPerSecondDuringArtificialIntelligenceCombat()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        yield return new WaitForSeconds(10);
        GameObject.Destroy(fpsTracker);
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        var hero = GameObject.Find("Hero");
        var navMeshAgent = hero.gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.destination = new Vector3(-19.5f, 0, -2.3f);
        fpsTracker = new GameObject();
        var tracker = fpsTracker.AddComponent<FPSTracker>();
        tracker.MinimumFPS = 50f;
    }
}
