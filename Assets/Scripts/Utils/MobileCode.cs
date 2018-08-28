using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCode
{
    private static Dictionary<string, string> codes = new Dictionary<string, string>();

    public static Dictionary<string, string> GetCodes()
    {
        if (codes.Count == 0)
        {
            codes.Add("中国", "0086");
            codes.Add("中国香港", "00852");
            codes.Add("中国澳门", "00853");
            codes.Add("中国台湾", "00886");
        }
        //foreach (KeyValuePair<string, string> kvp in codes)
        //{
        //    Utils.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
        //}
        return codes;
    }
}
