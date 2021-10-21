using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private PlayTimeData _playTimeData;

    public PlayTimeData PlayTimeData
    {
        get => _playTimeData;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //　プレイ時間をセーブ
    public void SavePlayTimeData() {
        PlayerPrefs.SetString("PlayTimeData", _playTimeData.GetJsonData());
        PlayerPrefs.Save ();
        Debug.Log($"セーブ{_playTimeData.hours}時間{_playTimeData.minutes}分{_playTimeData.seconds}秒");
    }
    //　プレイ時間をロード
    public void LoadPlayTimeData()
    {
        if(PlayerPrefs.HasKey("PlayTimeData")) {
            _playTimeData = new PlayTimeData();
            var data = PlayerPrefs.GetString("PlayTimeData");
            JsonUtility.FromJsonOverwrite(data, _playTimeData);
            Debug.Log($"ロード{_playTimeData.hours}時間{_playTimeData.minutes}分{_playTimeData.seconds}秒");
        }
    }
}
