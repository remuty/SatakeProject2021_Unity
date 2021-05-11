using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    private float _generateTime;

    private float _time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _generateTime)
        {
            var i = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[i], transform.position, Quaternion.identity);
            _time = 0;
            _generateTime = Random.Range(4, 7);
        }
    }
}
