using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private Transform _startTransform;
    private Transform _endTransform;
    
    private RhythmManager _rhythmManager;
    private SpriteRenderer _renderer;
    
    private float _time;
    private float _speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        _rhythmManager = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>();
        _renderer = GetComponent<SpriteRenderer>();
        transform.position = _startTransform.position;
        transform.localScale = _startTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time < _speed)
        {
            _time += Time.deltaTime;
            var rate = _time / _speed;
            transform.position = Vector3.Lerp(_startTransform.position, _endTransform.position, rate);
            transform.localScale = Vector3.Lerp(_startTransform.localScale, _endTransform.localScale, rate);
            _renderer.color = new Color(1, 1, 1, rate);
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
