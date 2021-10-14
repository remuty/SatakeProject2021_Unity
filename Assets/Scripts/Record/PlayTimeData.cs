using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayTimeData : object
{
    public float seconds;
    public int minutes;
    public int hours;
    
    public string GetJsonData() {
        return JsonUtility.ToJson(this);
    }

    public void SetPlayTime(float s,int m,int h)
    {
        seconds = s;
        minutes = m;
        hours = h;
    }
}