using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float timeMin;
    [SerializeField] private float timeMax;

    private RhythmManager _rhythmManager;
    private float _generateTime;
    private float _time;

    private List<int> _laneList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        _rhythmManager = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>();
        for (int i = 0; i < 3; i++)
        {
            _laneList.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //waveが切り替わる間際では生成しない
        if (_rhythmManager.Audio.time < _rhythmManager.Audio.clip.length - 10)
        {
            _time += Time.deltaTime;
            if (_time > _generateTime)
            {
                if (_laneList.Count > 0)
                {
                    var i = Random.Range(0, enemyPrefabs.Length);
                    var generatedEnemy = Instantiate(enemyPrefabs[i],
                        transform.position, Quaternion.identity, this.transform);
                    i = Random.Range(0, _laneList.Count);
                    generatedEnemy.GetComponent<NormalEnemy>().Lane = _laneList[i];
                    _laneList.RemoveAt(i);
                }
                _time = 0;
                //waveが増えるごとに生成時間が早くなる
                _generateTime = Random.Range(timeMin, timeMax) - _rhythmManager.Wave;
            }
        }
    }

    public void AddLane(int usedLane)
    {
        _laneList.Add(usedLane);
    }
}
