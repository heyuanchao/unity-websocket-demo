using DG.Tweening;
using LitJson;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public delegate void HttpCallback(JsonData jd);

public class Utils
{
    public static void Log(object msg)
    {
        Debug.Log(System.DateTime.Now.ToString("yyyy/M/d HH:mm:ss") + " " + msg);
    }

    public static void Quit()
    {
        Global.gsws.Disconnect();

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

    public static string GetAccount()
    {
        return PlayerPrefs.GetString("Account", "");
    }
    public static void SetAccount(string value)
    {
        Global.account = value;
        PlayerPrefs.SetString("Account", value);
    }

    public static string GetToken()
    {
        return PlayerPrefs.GetString("Token", "");
    }

    public static void SetToken(string value)
    {
        Global.token = value;
        PlayerPrefs.SetString("Token", value);
    }

    public static string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
