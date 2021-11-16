using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private string _key = "RecordData";
    private string _key2 = "SaveData";
    private RecordData _recordData;
    private SaveData _saveData;
    public RecordData RecordData => _recordData;
    public SaveData SaveData => _saveData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //　セーブ
    public void Save() {
        PlayerPrefs.SetString(_key, _recordData.GetJsonData());
        PlayerPrefs.SetString(_key2, _saveData.GetJsonData());
        PlayerPrefs.Save();
        Debug.Log($"セーブ{_recordData.hours}時間{_recordData.minutes}分" +
                  $"{_recordData.seconds}秒{_recordData.calorie}kcal{_recordData.date}");
    }
    //　ロード
    public void Load()
    {
        _recordData = new RecordData();
        if(PlayerPrefs.HasKey(_key)) {
            var data = PlayerPrefs.GetString(_key);
            JsonUtility.FromJsonOverwrite(data, _recordData);
            Debug.Log($"ロード{_recordData.hours}時間{_recordData.minutes}分" +
                      $"{_recordData.seconds}秒{_recordData.calorie}kcal{_recordData.date}");
        }
        _saveData = new SaveData();
        _saveData.recordDataList = new List<RecordData>();
        if(PlayerPrefs.HasKey(_key2)) {
            var data = PlayerPrefs.GetString(_key2);
            JsonUtility.FromJsonOverwrite(data, _saveData);

            var list = _saveData.recordDataList[0];
            Debug.Log(new {list.hours,list.minutes,list.seconds,list.calorie,list.date});
        }
    }
}
