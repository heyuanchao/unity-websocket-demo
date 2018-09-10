public class Config
{
    private const string localServHost = "192.168.11.24";
    private const string remoteServHost = "game.hedr.top";

    // private const string gameServHost = localServHost;
    private const string gameServHost = remoteServHost;

    public const string gameServWSAddr = "ws://" + gameServHost + ":8000";
    public const string gameServHTTPAddr = "http://" + gameServHost + ":8001";

    public const string checkAccountAddr = gameServHTTPAddr + "/check";
    public const string resetPasswordAddr = gameServHTTPAddr + "/reset";

    public static string GetCheckAccountUrl(string account, string lang)
    {
        // http://192.168.8.103:8001/check?account=15071334753&lang=zh
        return Config.checkAccountAddr + "?account=" + account + "&lang=" + lang;
    }

    public static string GetResetPasswordUrl(string account, string password, string smsCode, string lang)
    {
        // http://192.168.8.103:8001/reset?account=15071334753&password=654321&smsCode=6666&lang=zh
        return Config.resetPasswordAddr + "?account=" + account + "&password=" + password + "&smsCode=" + smsCode + "&lang=" + lang;
    }
}
