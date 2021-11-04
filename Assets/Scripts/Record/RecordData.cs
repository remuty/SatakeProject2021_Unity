using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RecordData : object
{
    public float seconds;
    public int minutes;
    public int hours;
    public int calorie;
    
    public string GetJsonData() {
        return JsonUtility.ToJson(this);
    }

    public void SetRecord(float s,int m,int h,int c)
    {
        seconds = s;
        minutes = m;
        hours = h;
        calorie = c;
    }
}
