using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private Transform _startTransform;
    [SerializeField] private Transform _endTransform;
    private SpriteRenderer _renderer;
    
    private float _time;
    private float _speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
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
            _renderer.enabled = true;
            _time = 0;
        }
    }

    public void SetTransform(Transform start,Transform end)
    {
        _startTransform = start;
        _endTransform = end;
    }
}
