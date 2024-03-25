using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSTracker : MonoBehaviour {

    float responseTimeInMilliseconds = 0f;
    float timePassed = 0f;
    public float StartupDelayInSeconds = 5f;
    public float? MinimumFPS = null;
	
	void Update () {
        responseTimeInMilliseconds = Time.unscaledDeltaTime * 1000f;
        if (timePassed < StartupDelayInSeconds)
            timePassed += Time.unscaledDeltaTime;
	}

    private void OnGUI()
    {
        if (timePassed < StartupDelayInSeconds)
            return; // Do nothing until the startup delay passes.

        var style = new GUIStyle();
        var rect = new Rect(0, 0, Screen.width, Screen.height / 10);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = Screen.height / 10;
        style.normal.textColor = Color.gray;
        var fps = 1f / (responseTimeInMilliseconds / 1000f);
        if (MinimumFPS.HasValue && fps < MinimumFPS.Value)
            Debug.LogErrorFormat("FPS ({0}) is below the minimum threshold of {1}!", fps, MinimumFPS.Value);
        var text = string.Format("{0:0.000} ms ({1:0.000} fps)", responseTimeInMilliseconds, fps);
        GUI.Label(rect, text, style);
    }
}
