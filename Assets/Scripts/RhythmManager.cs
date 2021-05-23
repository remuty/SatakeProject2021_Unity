using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;

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
            _time = 0;
            _generateTime = 0.5f;
        }
    }
}
