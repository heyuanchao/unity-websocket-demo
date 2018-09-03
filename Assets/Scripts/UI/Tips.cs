using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips
{

    private GameObject tips;
    private Text tipsText;

    // Use this for initialization
    public void Init()
    {
        tips = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Tips"));
        tipsText = tips.transform.Find("bg/Text").GetComponent<Text>();
        // tips.transform.parent = canvas.transform;
        tips.transform.SetParent(GameObject.Find("Canvas").transform, false);
        tips.SetActive(false);
    }

    public void Show(string text)
    {
        Show(text, null);
    }

    public void Show(string text, Action action)
    {
        MainThread.Run(() =>
        {
            if (tips == null)
            {
                Debug.Log("aaa");
            }
            tips.SetActive(true);
            tipsText.text = text;

            Utils.DelayRun2(3f, () =>
            {
                tips.SetActive(false);
                action.Invoke();
            });
        });
    }
}
