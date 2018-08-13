using UnityEngine;
using UnityEngine.SceneManagement;

public class Login_Btn : MonoBehaviour
{
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
        MyWebSocket.instance.Connect(Config.servAddr);
    }
}
