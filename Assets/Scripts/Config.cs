public class Config
{
    // private const string localServHost = "192.168.8.187";
    private const string localServHost = "192.168.11.24";
    private const string remoteServHost = "game.hedr.top";

    private const string gameServHost = localServHost;
    // private const string gameServHost = remoteServHost;

    public const string gameServWSAddr = "ws://" + gameServHost + ":8000";
    public const string gameServHTTPAddr = "http://" + gameServHost + ":8001";

    public const string checkAccountUrl = gameServHTTPAddr + "/check";
    public const string resetPasswordUrl = gameServHTTPAddr + "/reset";

    public const string registrationSmsCodeUrl = gameServHTTPAddr + "/registrationSmsCode";
    public const string consumptionSmsCodeUrl = gameServHTTPAddr + "/consumptionSmsCode";

    public const string serverListUrl = gameServHTTPAddr + "/serverList";

    public static string GetCheckAccountUrl(string account, string lang)
    {
        // http://localhost:8001/check?account=15071334753&lang=zh
        return checkAccountUrl + "?account=" + account + "&lang=" + lang;
    }

    public static string GetResetPasswordUrl(string account, string password, string smsCode, string lang)
    {
        // http://localhost:8001/reset?account=15071334753&password=654321&smsCode=6666&lang=zh
        return resetPasswordUrl + "?account=" + account + "&password=" + password + "&smsCode=" + smsCode + "&lang=" + lang;
    }

    public static string GetRegistrationSmsCodeUrl(string account, string mobileCode)
    {
        // http://localhost:8001/registrationSmsCode?account=15071334753&mobileCode=86
        return registrationSmsCodeUrl + "?account=" + account + "&mobileCode=" + mobileCode;
    }

    public static string GetConsumptionSmsCodeUrl(string account, string mobileCode)
    {
        // http://localhost:8001/consumptionSmsCode?account=15071334753&mobileCode=86
        return consumptionSmsCodeUrl + "?account=" + account + "&mobileCode=" + mobileCode;
    }
}
