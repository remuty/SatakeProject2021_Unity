using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private Transform _startTransform;
    private Transform _endTransform;
    
    private RhythmManager _rhythmManager;
    
    private float _time;
    private float _speed = 1.6f;
    // Start is called before the first frame update
    void Start()
    {
        _rhythmManager = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>();
        transform.position = _startTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time < _speed)
        {
            _time += Time.deltaTime;
            var rate = _time / _speed;
            transform.position = Vector3.Lerp(_startTransform.position, _endTransform.position, rate);
        }
        else
        {
            _rhythmManager.RemoveNote();
            Destroy(this.gameObject);
        }
    }

    public void SetTransform(Transform start,Transform end)
    {
        _startTransform = start;
        _endTransform = end;
    }
}
