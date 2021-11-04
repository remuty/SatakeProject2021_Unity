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
    private float _seconds;
    private int _minutes;
    private int _hours;
    private int _calorie;
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
            _seconds = _recordData.seconds;
            _minutes = _recordData.minutes;
            _hours = _recordData.hours;
            _calorie = _recordData.calorie;
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
            _seconds += Time.deltaTime + 1;
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
        else if (_switchScene.Scene == SwitchScene.Scenes.Result)
        {
            //セーブ
            if (!_isSaved)
            {
                _isSaved = true;
                //カロリー計算 5メッツ*60kg*時間*1.05
                var time = _hours + _minutes / 60f;
                _calorie = (int)(5 * 60 * time * 1.05);
                _recordData.SetRecord(_seconds, _minutes, _hours,_calorie,_date);
                _recordDataList.Add(_recordData);
                _saveLoad.SaveData.SetRecordList(_recordDataList);
                _saveLoad.Save();
            }
        }
        else if (_switchScene.Scene == SwitchScene.Scenes.Home)
        {
            //プレイ時間とカロリー表示
            GameObject.Find("Minutes").GetComponent<Text>().text = $"{_minutes}";
            GameObject.Find("Hours").GetComponent<Text>().text = $"{_hours}";
            GameObject.Find("Calorie").GetComponent<Text>().text = $"{_calorie}";
        }
    }
}
