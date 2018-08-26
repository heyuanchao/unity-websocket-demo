using System;
using System.Collections;
using UnityEngine;

public class Util
{
    public static void Quit()
    {
        Config.gsws.Disconnect();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public static IEnumerator DelayRun(Action action, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }
}
