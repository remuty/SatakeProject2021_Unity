using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Record : MonoBehaviour
{
    private SwitchScene _switchScene;
    private SaveLoad _saveLoad;
    private RecordData _recordData;
    private List<RecordData> _recordDataList;
    private float _seconds, _secondsSum, _time;
    private int _minutes, _minutesSum, _hours, _hoursSum, _calorie, _calorieSum, _score, _wave, _exp;

    private bool _isSaved;
    private string _date = DateTime.Today.ToLongDateString();

    // Start is called before the first frame update
    void Start()
    {
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
        _saveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();
        _saveLoad.Load();
        _recordData = _saveLoad.RecordData;
        _recordDataList = _saveLoad.SaveData.recordDataList;
        //セーブデータの日付が今日ならロード
        if (_recordData.date == _date)
        {
            _secondsSum = _recordData.seconds;
            _minutesSum = _recordData.minutes;
            _hoursSum = _recordData.hours;
            _calorieSum = _recordData.calorie;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_switchScene.Scene == SwitchScene.Scenes.Main)
        {
            //プレイ時間計測
            _seconds += Time.deltaTime;
            if (_seconds >= 60f)
            {
                _minutes++;
                _seconds = _seconds - 60;
                if (_minutes >= 60)
                {
                    _hours++;
                    _minutes = 0;
                }
            }
        }
        else if (_switchScene.Scene == SwitchScene.Scenes.Result0)
        {
            //セーブ
            if (!_isSaved)
            {
                _isSaved = true;
                //カロリー計算 5メッツ*60kg*時間*1.05
                var time = _hours + _minutes / 60f;
                _calorie = (int) (5 * 60 * time * 1.05);
                //得点計算 waveごとに500点　10秒ごとに2点
                _wave = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>().Wave;
                _score += 500 * _wave;
                _score += (int) (_minutes * 60 + _seconds) / 10 * 2;
                //経験値計算　得点*0.3
                _exp = (int) (_score * 0.3);

                _secondsSum += _seconds;
                _minutesSum += _minutes;
                _hoursSum += _hours;
                _calorieSum += _calorie;
                _recordData.SetRecord(_secondsSum, _minutesSum, _hoursSum, _calorieSum, _date);
                _recordDataList.Add(_recordData);
                _saveLoad.SaveData.SetRecordList(_recordDataList);
                _saveLoad.Save();
            }

            //得点、最大波、運動時間、消費カロリー表示
            GameObject.Find("Score").GetComponent<Text>().text = $"{StringWidthConverter.IntToFull(_score)}点";
            GameObject.Find("Wave").GetComponent<Text>().text = $"{StringWidthConverter.IntToFull(_wave)}点";
            GameObject.Find("Time").GetComponent<Text>().text = $"{StringWidthConverter.IntToFull(_minutes)}分"+
                $"{StringWidthConverter.IntToFull((int) _seconds)}秒";
            GameObject.Find("Calorie").GetComponent<Text>().text = StringWidthConverter.IntToFull(_calorie);
        }
        else if (_switchScene.Scene == SwitchScene.Scenes.Result1)
        {
            if (_isSaved)
            {
                //経験値表示
                GameObject.Find("EXP").GetComponent<Text>().text = StringWidthConverter.IntToFull(_exp);
                var bar = GameObject.Find("EXPBar").GetComponent<Image>();
                if (_time < 0.5f)
                {
                    _time += Time.unscaledDeltaTime;
                    bar.fillAmount = _time / 0.5f;
                }
                else
                {
                    bar.fillAmount = 0.2f;
                    GameObject.Find("ResultPanel").transform.Find("Result2").gameObject.SetActive(true);
                    _switchScene.Scene = SwitchScene.Scenes.Result2;
                    GameObject.Find("Result1").SetActive(false);
                    Reset();
                }
            }
        }
        else if (_switchScene.Scene == SwitchScene.Scenes.Home)
        {
            //ホーム画面プレイ時間とカロリー表示
            GameObject.Find("Minutes").GetComponent<Text>().text = StringWidthConverter.IntToFull(_minutesSum);
            GameObject.Find("Hours").GetComponent<Text>().text = StringWidthConverter.IntToFull(_hoursSum);
            GameObject.Find("Calorie").GetComponent<Text>().text = StringWidthConverter.IntToFull(_calorieSum);
        }
    }

    void Reset()
    {
        _seconds = 0;
        _secondsSum = 0;
        _time = 0;
        _minutes = 0;
        _hours = 0;
        _calorie = 0;
        _score = 0;
        _exp = 0;
        _isSaved = false;
    }

    public void AddScore(int point)
    {
        _score += point;
    }
    
    //文字列を全角に変換
    string StrConvToFull(string halfWidthStr)
    {
        string fullWidthStr = null;

        for (int i = 0; i < halfWidthStr.Length; i++)
        {
            fullWidthStr += (char)(halfWidthStr[i] + 65248);
        }

        return fullWidthStr;
    }
}