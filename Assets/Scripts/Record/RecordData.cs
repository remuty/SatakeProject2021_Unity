using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RecordData : object
{
    public int seconds;
    public int minutes;
    public int hours;
    public int calorie;
    public int score;
    public int wave;
    public int day;
    public int month;

    public string GetJsonData()
    {
        return JsonUtility.ToJson(this);
    }

    public void SetRecord(int s, int m, int h, int c, int p, int w, DateTime d)
    {
        seconds = s;
        minutes = m;
        hours = h;
        calorie = c;
        score = p;
        wave = w;
        day = d.Day;
        month = d.Month;
    }

    //Debug.Log出力用
    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}