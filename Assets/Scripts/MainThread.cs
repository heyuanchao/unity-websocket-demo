using System;
using System.Collections.Generic;
using UnityEngine;

internal static class MainThread
{
    private static MainThreadHelper helper;

    public static void Init()
    {
        var obj = GameObject.Find("MainThreadHelper");
        if (obj == null)
        {
            helper = (new GameObject("MainThreadHelper")).AddComponent<MainThreadHelper>();
        }
    }

    public static void Run(Action action)
    {
        helper.AddTask(action);
    }
}

public sealed class MainThreadHelper : MonoBehaviour
{
    public Queue<Action> queue = new Queue<Action>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        lock (queue)
        {
            if (queue.Count > 0)
            {
                queue.Dequeue().Invoke();
            }
        }
    }

    public void AddTask(Action action)
    {
        lock (queue)
        {
            queue.Enqueue(action);
        }
    }
}
