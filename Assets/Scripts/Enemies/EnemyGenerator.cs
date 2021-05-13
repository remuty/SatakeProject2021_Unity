using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    private float _generateTime;

    private float _time;

    private int _wave;

    public int Wave
    {
        set { _wave = value; }
    }

    private List<int> _laneList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            _laneList.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _generateTime)
        {
            if (_laneList.Count > 0)
            {
                var i = Random.Range(0, enemyPrefabs.Length);
                var generatedEnemy = Instantiate(enemyPrefabs[i], transform.position, Quaternion.identity);
                i = Random.Range(0, _laneList.Count);
                generatedEnemy.GetComponent<NormalEnemy>().Lane = _laneList[i];
                _laneList.RemoveAt(i);
            }
            _time = 0;
            _generateTime = Random.Range(3, 5) - _wave;
        }
    }

    public void AddLane(int usedLane)
    {
        _laneList.Add(usedLane);
    }
}
