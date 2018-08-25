using UnityEngine;

public class Util
{
    public static void Quit()
    {
        MyWebSocket.instance.Disconnect();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
