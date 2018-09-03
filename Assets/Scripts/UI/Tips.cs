using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips
{
    private GameObject prefab;
    private Text tipsText;

    // Use this for initialization
    public Tips()
    {
        prefab = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Tips"));
        tipsText = prefab.transform.Find("bg/Text").GetComponent<Text>();
        // tips.transform.parent = canvas.transform;
        prefab.transform.SetParent(GameObject.Find("Canvas").transform, false);
        prefab.SetActive(false);
    }

    public void Show(string text)
    {
        Show(text, null);
    }

    public void Show(string text, Action action)
    {
        MainThread.Run(() =>
        {
            prefab.SetActive(true);
            tipsText.text = text;

            Utils.DelayRun2(3f, () =>
            {
                prefab.SetActive(false);
                action.Invoke();
            });
        });
    }
}
