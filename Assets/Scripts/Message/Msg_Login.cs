using LitJson;

public class C2S_Login
{
    public static string msgName = "C2S_Login";
    public JsonData jsonData = new JsonData();

    public C2S_Login(string account, string password)
    {
        var d = new JsonData();
        d["Account"] = account;
        d["Password"] = password;

        this.jsonData[msgName] = d;
    }
}

public class S2C_Login
{
    public static string msgName = "S2C_Login";
}
