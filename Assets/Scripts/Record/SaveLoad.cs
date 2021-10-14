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
        _playTimeData = new PlayTimeData();
    }

    //　プレイ時間をセーブ
    public void SavePlayTimeData() {
        PlayerPrefs.SetString("PlayTimeData", _playTimeData.GetJsonData());
    }
    //　プレイ時間をロード
    public void LoadPlayTimeData() {
        if(PlayerPrefs.HasKey("PlayTimeData")) {
            var data = PlayerPrefs.GetString("PlayTimeData");
            JsonUtility.FromJsonOverwrite(data, _playTimeData);
        }
    }
}
