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
    private float _seconds, _secondsSum;
    private int _minutes, _minutesSum, _hours, _hoursSum, _calorie, _calorieSum;
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
            if (_isSaved)
            {
                _isSaved = false;
            }

            //プレイ時間計測
            _seconds += Time.deltaTime + 0.1f; //TODO:デバッグ用　0.1を消す
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
                _calorie = (int)(5 * 60 * time * 1.05);
                _secondsSum += _seconds;
                _minutesSum += _minutes;
                _hoursSum += _hours;
                _calorieSum += _calorie;
                _recordData.SetRecord(_secondsSum, _minutesSum, _hoursSum,_calorieSum,_date);
                _recordDataList.Add(_recordData);
                _saveLoad.SaveData.SetRecordList(_recordDataList);
                _saveLoad.Save();
            }
            //リザルト表示
            GameObject.Find("Time").GetComponent<Text>().text = $"{_minutes}分{(int)_seconds}秒";
            
        }
        else if (_switchScene.Scene == SwitchScene.Scenes.Result1)
        {
            //リザルト表示
            GameObject.Find("Calorie").GetComponent<Text>().text = $"{_calorie}kcal";
        }
        else if (_switchScene.Scene == SwitchScene.Scenes.Home)
        {
            //ホーム画面プレイ時間とカロリー表示
            GameObject.Find("Minutes").GetComponent<Text>().text = $"{_minutesSum}";
            GameObject.Find("Hours").GetComponent<Text>().text = $"{_hoursSum}";
            GameObject.Find("Calorie").GetComponent<Text>().text = $"{_calorieSum}";
        }
    }
}
