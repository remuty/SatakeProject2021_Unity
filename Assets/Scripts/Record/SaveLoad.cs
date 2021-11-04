using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private string _key = "RecordData";
    private RecordData _recordData;

    public RecordData RecordData => _recordData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //　セーブ
    public void Save() {
        PlayerPrefs.SetString(_key, _recordData.GetJsonData());
        PlayerPrefs.Save ();
        Debug.Log($"セーブ{_recordData.hours}時間{_recordData.minutes}分" +
                  $"{_recordData.seconds}秒{_recordData.calorie}kcal");
    }
    //　ロード
    public void Load()
    {
        _recordData = new RecordData();
        if(PlayerPrefs.HasKey(_key)) {
            var data = PlayerPrefs.GetString(_key);
            JsonUtility.FromJsonOverwrite(data, _recordData);
            Debug.Log($"ロード{_recordData.hours}時間{_recordData.minutes}分" +
                      $"{_recordData.seconds}秒{_recordData.calorie}kcal");
        }
    }
}
