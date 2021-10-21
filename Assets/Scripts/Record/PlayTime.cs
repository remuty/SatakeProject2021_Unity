using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTime : MonoBehaviour
{
    private SwitchScene _switchScene;
    private SaveLoad _saveLoad;
    private float _seconds;
    private int _minutes;
    private int _hours;
    private bool _isSaved;

    // Start is called before the first frame update
    void Start()
    {
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
        _saveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();
        _saveLoad.LoadPlayTimeData();
        _seconds = _saveLoad.PlayTimeData.seconds;
        _minutes = _saveLoad.PlayTimeData.minutes;
        _hours = _saveLoad.PlayTimeData.hours;
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
            if (!_isSaved)
            {
                _isSaved = true;
                _saveLoad.PlayTimeData.SetPlayTime(_seconds, _minutes, _hours);
                _saveLoad.SavePlayTimeData();
            }
        }
        else if (_switchScene.Scene == SwitchScene.Scenes.Home)
        {
            GameObject.Find("Minutes").GetComponent<Text>().text = $"{_minutes}";
            GameObject.Find("Hours").GetComponent<Text>().text = $"{_hours}";
        }

        // Debug.Log(_minutes + "分" + _seconds + "秒");
    }
}