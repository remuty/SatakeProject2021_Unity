using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    private SwitchScene _switchScene;
    private SaveLoad _saveLoad;
    private float _seconds;
    private int _minutes;
    private int _hours;
    private int _calorie;
    private bool _isSaved;

    // Start is called before the first frame update
    void Start()
    {
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
        _saveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();
        _saveLoad.Load();
        _seconds = _saveLoad.RecordData.seconds;
        _minutes = _saveLoad.RecordData.minutes;
        _hours = _saveLoad.RecordData.hours;
        _calorie = _saveLoad.RecordData.calorie;
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
        else if (_switchScene.Scene == SwitchScene.Scenes.Result)
        {
            //セーブ
            if (!_isSaved)
            {
                _isSaved = true;
                //カロリー計算
                var time = _hours + _minutes / 60f;
                _calorie = (int)(5 * 60 * time * 1.05);
                _saveLoad.RecordData.SetRecord(_seconds, _minutes, _hours,_calorie);
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
