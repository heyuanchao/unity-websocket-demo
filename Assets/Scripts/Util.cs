using DG.Tweening;
using LitJson;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public delegate void HttpCallback(JsonData jd);

public class Util
{
    public static void Log(object msg)
    {
        Debug.Log(System.DateTime.Now.ToString("yyyy/M/d HH:mm:ss") + " " + msg);
    }

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

    public static void DelayRun2(float seconds, Action action)
    {
        DOTween.Sequence().InsertCallback(seconds, new TweenCallback(() =>
        {
            action.Invoke();
        }));
    }

    public static string GetCheckAccountUrl(string account, string lang)
    {
        // http://192.168.8.103:8001/check?account=15071334753&lang=zh
        return Config.checkAccountAddr + "?account=" + account + "&lang=" + lang;
    }

    public static IEnumerator HttpGet(string url, HttpCallback callback = null)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("request url: " + url + " error: " + request.error);
            yield return null;
        }
        if (request.responseCode == 200)
        {
            string text = request.downloadHandler.text;
            // Debug.Log(text);
            JsonData jd = JsonMapper.ToObject(text);
            if (callback != null)
            {
                callback.Invoke(jd);
            }
        }
    }
}
