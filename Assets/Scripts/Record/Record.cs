using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Record : MonoBehaviour
{
    private SwitchScene _switchScene;
    private SoundManager _sound;
    private SaveLoad _saveLoad;
    private RecordData _recordData;
    private List<RecordData> _recordDataList;
    private float _seconds, _time;
    private int _minutes, _hours, _calorie, _score, _wave, _exp, _kill;
    private int _secondsToday, _minutesToday, _hoursToday, _calorieToday, _scoreToday, _waveToday; //今日のプレイ記録の合計
    private int _minutesSum, _hoursSum, _calorieSum; //全てのプレイ記録の合計
    private bool _isSaved;
    private DateTime _date = DateTime.Today.AddDays(0);

    // Start is called before the first frame update
    void Start()
    {
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
        _sound = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        _saveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();
        _saveLoad.Load();
        _recordData = _saveLoad.RecordData;
        _recordDataList = _saveLoad.SaveData.recordDataList;
        //最新のセーブデータの日付が今日ならロード
        var c = _recordDataList.Count;
        if (c > 0)
        {
            if (_recordDataList[c - 1].day == _date.Day && _recordDataList[c - 1].month == _date.Month)
            {
                _recordData = _recordDataList[c - 1];
                _secondsToday = _recordData.seconds;
                _minutesToday = _recordData.minutes;
                _hoursToday = _recordData.hours;
                _calorieToday = _recordData.calorie;
                _scoreToday = _recordData.score;
                _waveToday = _recordData.wave;
            }
        }

        Sum();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_switchScene.Scene)
        {
            case SwitchScene.Scenes.Main:
                MainScene();
                break;
            case SwitchScene.Scenes.Result0:
                Result0Scene();
                break;
            case SwitchScene.Scenes.Result1:
                Result1Scene();
                break;
            case SwitchScene.Scenes.Home:
                HomeScene();
                break;
            case SwitchScene.Scenes.Record:
                RecordScene();
                break;
            case SwitchScene.Scenes.Card:
                CardScene();
                break;
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
        foreach (var data in _recordDataList)
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
        _kill++;
    }

    void MainScene()
    {
        //データリセット
        if (_isSaved)
        {
            Reset();
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

    void Result0Scene()
    {
        //セーブ
        if (!_isSaved)
        {
            _isSaved = true;
            //カロリー計算 5メッツ*60kg*時間*1.05
            var time = _hours + _minutes / 60f;
            _calorie = (int)(5 * 60 * time * 1.05);
            //得点計算 waveごとに500点　10秒ごとに2点
            _wave = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>().Wave;
            _score += 500 * _wave;
            _score += (int)(_minutes * 60 + _seconds) / 10 * 2;
            //経験値計算　得点*0.3
            _exp = (int)(_score * 0.3);

            //今日の合計計算
            _secondsToday += (int)_seconds;
            _minutesToday += (_minutes + _secondsToday / 60) % 60;
            _secondsToday = _secondsToday % 60;
            _hoursToday += _hours + _minutesToday / 60;
            _calorieToday += _calorie;
            _scoreToday += _score;
            _waveToday += _wave;
            _recordData.SetRecord(_secondsToday, _minutesToday, _hoursToday,
                _calorieToday, _scoreToday, _waveToday, _date);
            //セーブデータ上書きor追加
            var c = _recordDataList.Count;
            if (c > 0)
            {
                if (_recordDataList[c - 1].day == _date.Day && _recordDataList[c - 1].month == _date.Month)
                {
                    _recordDataList[c - 1] = _recordData;
                }
                else
                {
                    _recordDataList.Add(_recordData);
                }
            }
            else
            {
                _recordDataList.Add(_recordData);
            }

            _saveLoad.SaveData.SetRecordList(_recordDataList);
            _saveLoad.Save();

            Sum();

            //得点、最大波、運動時間、消費カロリー表示
            GameObject.Find("Score").GetComponent<Text>().text = $"{StringWidthConverter.IntToFull(_score)}点";
            GameObject.Find("Wave").GetComponent<Text>().text = $"{StringWidthConverter.IntToFull(_wave)}波";
            GameObject.Find("Time").GetComponent<Text>().text = $"{StringWidthConverter.IntToFull(_minutes)}分" +
                                                                $"{StringWidthConverter.IntToFull((int)_seconds)}秒";
            GameObject.Find("Calorie").GetComponent<Text>().text = StringWidthConverter.IntToFull(_calorie);
        }
    }

    void Result1Scene()
    {
        if (_isSaved)
        {
            //経験値表示
            GameObject.Find("EXP").GetComponent<Text>().text = StringWidthConverter.IntToFull(_exp);
            var bar = GameObject.Find("EXPBar").GetComponent<Image>();
            _sound.ExpUp();
            if (_time < 0.8f)
            {
                _time += Time.unscaledDeltaTime;
                bar.fillAmount = _time / 0.8f;
            }
            else
            {
                _sound.LevelUp();
                bar.fillAmount = 0.2f;
                GameObject.Find("ResultPanel").transform.Find("Result2").gameObject.SetActive(true);
                _switchScene.Scene = SwitchScene.Scenes.Result2;
                GameObject.Find("Result1").SetActive(false);
                _sound.GettingCard();
                Reset();
            }
        }
    }

    void HomeScene()
    {
        //ホーム画面：今日のプレイ時間とカロリー表示
        GameObject.Find("Minutes").GetComponent<Text>().text = StringWidthConverter.IntToFull(_minutesToday);
        GameObject.Find("Hours").GetComponent<Text>().text = StringWidthConverter.IntToFull(_hoursToday);
        GameObject.Find("Calorie").GetComponent<Text>().text = StringWidthConverter.IntToFull(_calorieToday);
        //ホーム画面：デイリーチャレンジ
        if (_kill >= 8)
        {
            GameObject.Find("Challeng0").GetComponent<Image>().enabled = false;
            GameObject.Find("Achieved0").GetComponent<Image>().enabled = true;
        }
        else
        {
            GameObject.Find("Challeng0").GetComponent<Image>().enabled = true;
            GameObject.Find("Achieved0").GetComponent<Image>().enabled = false;
        }

        if (_minutesToday >= 20 || _hoursToday >= 1)
        {
            GameObject.Find("Challeng1").GetComponent<Image>().enabled = false;
            GameObject.Find("Achieved1").GetComponent<Image>().enabled = true;
        }
        else
        {
            GameObject.Find("Challeng1").GetComponent<Image>().enabled = true;
            GameObject.Find("Achieved1").GetComponent<Image>().enabled = false;
        }

        if (_waveToday >= 8)
        {
            GameObject.Find("Challeng2").GetComponent<Image>().enabled = false;
            GameObject.Find("Achieved2").GetComponent<Image>().enabled = true;
        }
        else
        {
            GameObject.Find("Challeng2").GetComponent<Image>().enabled = true;
            GameObject.Find("Achieved2").GetComponent<Image>().enabled = false;
        }
    }

    void RecordScene()
    {
        //記録画面：合計のプレイ時間とカロリー表示
        GameObject.Find("MinutesSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_minutesSum);
        GameObject.Find("HoursSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_hoursSum);
        GameObject.Find("CalorieSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_calorieSum);
        //記録画面：日付ごとに最新の３日間の記録表示
        if (_recordDataList.Count >= 1)
        {
            RecordByDate(0);
        }

        if (_recordDataList.Count >= 2)
        {
            RecordByDate(1);
        }

        if (_recordDataList.Count >= 3)
        {
            RecordByDate(2);
        }
    }

    void CardScene()
    {
        //所持札一覧画面：合計のプレイ時間とカロリー表示
        GameObject.Find("MinutesSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_minutesSum);
        GameObject.Find("HoursSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_hoursSum);
        GameObject.Find("CalorieSum").GetComponent<Text>().text = StringWidthConverter.IntToFull(_calorieSum);
    }

    void RecordByDate(int i)
    {
        var c = _recordDataList.Count;
        GameObject.Find($"Month{i}").GetComponent<Text>().text =
            StringWidthConverter.IntToFull(_recordDataList[c - 1 - i].month);
        GameObject.Find($"Day{i}").GetComponent<Text>().text =
            StringWidthConverter.IntToFull(_recordDataList[c - 1 - i].day);
        GameObject.Find($"Score{i}").GetComponent<Text>().text =
            StringWidthConverter.IntToFull(_recordDataList[c - 1 - i].score);
        GameObject.Find($"Wave{i}").GetComponent<Text>().text =
            StringWidthConverter.IntToFull(_recordDataList[c - 1 - i].wave);
        GameObject.Find($"Hours{i}").GetComponent<Text>().text =
            StringWidthConverter.IntToFull(_recordDataList[c - 1 - i].hours);
        GameObject.Find($"Minutes{i}").GetComponent<Text>().text =
            StringWidthConverter.IntToFull(_recordDataList[c - 1 - i].minutes);
        GameObject.Find($"Calorie{i}").GetComponent<Text>().text =
            StringWidthConverter.IntToFull(_recordDataList[c - 1 - i].calorie);
    }
}