using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTime : MonoBehaviour
{
    private SwitchScene _switchScene;
    private float _seconds;
    private int _minutes;
    private int _hours;

    // Start is called before the first frame update
    void Start()
    {
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_switchScene.Scene == SwitchScene.Scenes.Main)
        {
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
        else if (_switchScene.Scene == SwitchScene.Scenes.Home)
        {
            GameObject.Find("Minutes").GetComponent<Text>().text = $"{_minutes}";
            GameObject.Find("Hours").GetComponent<Text>().text = $"{_hours}";
        }

        Debug.Log(_minutes + "分" + _seconds + "秒");
    }
}