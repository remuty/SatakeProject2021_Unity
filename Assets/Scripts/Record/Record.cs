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
    private float _seconds, _time;
    private int _minutes, _hours, _calorie, _score, _wave, _exp;
    private int _secondsToday, _minutesToday, _hoursToday, _calorieToday;   //今日のプレイ記録の合計
    private int _minutesSum, _hoursSum, _calorieSum;    //全てのプレイ記録の合計
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
            _secondsToday = _recordData.seconds;
            _minutesToday = _recordData.minutes;
            _hoursToday = _recordData.hours;
            _calorieToday = _recordData.calorie;
        }

        Sum();
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

                _secondsToday += (int)_seconds;
                _minutesToday += _minutes;
                _hoursToday += _hours;
                _calorieToday += _calorie;
                _recordData.SetRecord(_secondsToday, _minutesToday, _hoursToday, _calorieToday, _date);
                _recordDataList.Add(_recordData);
                _saveLoad.SaveData.SetRecordList(_recordDataList);
                _saveLoad.Save();

                Sum();
            }

            //得点、最大波、運動時間、消費カロリー表示
            GameObject.Find("Score").GetComponent<Text>().text = $"{StringWidthConverter.IntToFull(_score)}点";
            GameObject.Find("Wave").GetComponent<Text>().text = $"{StringWidthConverter.IntToFull(_wave)}点";
            GameObject.Find("Time").GetComponent<Text>().text = $"{StringWidthConverter.IntToFull(_minutes)}分" +
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
            //ホーム画面：今日のプレイ時間とカロリー表示
            GameObject.Find("Minutes").GetComponent<Text>().text = StringWidthConverter.IntToFull(_minutesToday);
            GameObject.Find("Hours").GetComponent<Text>().text = StringWidthConverter.IntToFull(_hoursToday);
            GameObject.Find("Calorie").GetComponent<Text>().text = StringWidthConverter.IntToFull(_calorieToday);
        }
        else if (_switchScene.Scene == SwitchScene.Scenes.Record)
        {
            //記録画面：合計のプレイ時間とカロリー表示
            GameObject.Find("MinutesSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_minutesSum);
            GameObject.Find("HoursSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_hoursSum);
            GameObject.Find("CalorieSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_calorieSum);
        }
        else if (_switchScene.Scene == SwitchScene.Scenes.Card)
        {
            //所持札一覧画面：合計のプレイ時間とカロリー表示
            GameObject.Find("MinutesSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_minutesSum);
            GameObject.Find("HoursSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_hoursSum);
            GameObject.Find("CalorieSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_calorieSum);
        }
    }

    void Reset()
    {
        _seconds = 0;
        _time = 0;
        _minutes = 0;
        _hours = 0;
        _calorie = 0;
        _score = 0;
        _exp = 0;
        _isSaved = false;
    }

    void Sum()
    {
        //全てのプレイ記録の合計
        _minutesSum = 0;
        _hoursSum = 0;
        _calorieSum = 0;
        foreach(var data in _recordDataList)
        {
            _minutesSum += data.minutes;
            _hoursSum += data.hours;
            _calorieSum += data.calorie;
        }
        _hoursSum += _minutesSum / 60;
        _minutesSum = _minutesSum % 60;
    }

    public void AddScore(int point)
    {
        _score += point;
    }
}