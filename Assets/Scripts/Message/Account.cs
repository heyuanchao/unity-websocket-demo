using LitJson;

public class C2S_Login
{
    public static readonly string msgName = "C2S_Login";
    private JsonData jsonData = new JsonData();

    public JsonData CreatePasswordLoginMsg(string account, string password)
    {
        var jd = new JsonData();
        jd["Account"] = account;
        jd["Password"] = password;

        jsonData[msgName] = jd;
        return jsonData;
    }

    public JsonData CreateSmsCodeLoginMsg(string account, string smsCode)
    {
        var jd = new JsonData();
        jd["Account"] = account;
        jd["SmsCode"] = smsCode;

        jsonData[msgName] = jd;
        return jsonData;
    }
}

public class S2C_Login
{
    public static readonly string msgName = "S2C_Login";
}

public class C2S_Register
{
    public static readonly string msgName = "C2S_Register";
    private JsonData jsonData = new JsonData();

    public JsonData CreateRegisterMsg(string account, string password, string smsCode, string invitationCode)
    {
        var jd = new JsonData();
        jd["Account"] = account;
        jd["Password"] = password;
        jd["SmsCode"] = smsCode;
        jd["InvitationCode"] = invitationCode;

        jsonData[msgName] = jd;
        return jsonData;
    }
}
