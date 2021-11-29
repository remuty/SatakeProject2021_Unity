using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData : object
{
    public List<RecordData> recordDataList;
    
    public string GetJsonData() {
        return JsonUtility.ToJson(this);
    }
    
    public void SetRecordList(List<RecordData> r)
    {
        recordDataList = r;
    }
    
    //Debug.Log出力用
    public override string ToString()
    {
        return JsonUtility.ToJson( this, true );
    }
}
