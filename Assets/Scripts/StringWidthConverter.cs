using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringWidthConverter : MonoBehaviour
{
    const int ConvertionConstant = 65248;

    //文字列を全角に変換
    static public string ToFull(string halfWidthStr)
    {
        string fullWidthStr = null;

        for (int i = 0; i < halfWidthStr.Length; i++)
        {
            fullWidthStr += (char)(halfWidthStr[i] + ConvertionConstant);
        }

        return fullWidthStr;
    }

    //文字列を半角に変換
    static public string ToHalf(string fullWidthStr)
    {
        string halfWidthStr = null;

        for (int i = 0; i < fullWidthStr.Length; i++)
        {
            halfWidthStr += (char)(fullWidthStr[i] - ConvertionConstant);
        }

        return halfWidthStr;
    }
    
    //整数を全角に変換
    static public string IntToFull(int halfWidthInt)
    {
        string halfWidthStr = halfWidthInt.ToString();
        string fullWidthStr = null;

        for (int i = 0; i < halfWidthStr.Length; i++)
        {
            fullWidthStr += (char)(halfWidthStr[i] + ConvertionConstant);
        }

        return fullWidthStr;
    }
}
