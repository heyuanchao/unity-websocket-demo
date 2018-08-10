using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login_Btn : MonoBehaviour
{
    public int count = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Login()
    {
        Debug.Log(count);
        count++;

        MyWebSocket.instance.Connect(Config.servAddr);
    }
}
