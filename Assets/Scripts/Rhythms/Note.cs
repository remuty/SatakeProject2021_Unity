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
    private SoundManager _sound;

    private float _time;
    private float _speed = 1.5f;

    private bool _isMissed;

    public bool IsMissed => _isMissed;

    private bool _isWarning;

    public bool IsWarning
    {
        set => _isWarning = value;
    }
    
    private bool _isAlerted;
    
    private bool _isBeated;

    public bool IsBeated
    {
        set => _isBeated = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rhythmManager = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>();
        _renderer = GetComponent<SpriteRenderer>();
        transform.position = _startTransform.position;
        transform.localScale = _startTransform.localScale;
        _sound = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time <= _speed)
        {
            //ノーツが両端から中央に移動する
            var rate = _time / _speed;
            transform.position = Vector3.Lerp(_startTransform.position, _endTransform.position, rate);
            transform.localScale = Vector3.Lerp(_startTransform.localScale, _endTransform.localScale, rate);
            _renderer.color = new Color(1, 1, 1, rate);
        }
        else if (_time < _speed + 0.2f)
        {
            //敵の攻撃中は警告音をタイミングよく鳴らす
            if (_isWarning && !_isAlerted)
            {
                _sound.Alert();
                _isAlerted = true;
            }
            transform.position = _endTransform.position;
        }
        else if (_time < _speed + 0.22f)
        {
            _isMissed = true;
        }
        else
        {
            if (!_isBeated)
            {
                _rhythmManager.RemoveNote();
            }
            Destroy(this.gameObject);
        }
    }

    public void SetTransform(Transform start, Transform end)
    {
        _startTransform = start;
        _endTransform = end;
    }
}