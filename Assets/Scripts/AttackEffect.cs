using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    private float _time;
    private Vector2 _initialPosition;
    private Vector2 _initialScale;
    private GameObject _target;
    public GameObject Target
    {
        set => _target = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = this.transform.position;
        _initialScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time < 0.3f)
        {
            _time += Time.deltaTime;
            var rate = _time / 0.3f;
            transform.position = Vector2.Lerp(_initialPosition,
                _target.transform.position, rate);
            transform.localScale = Vector2.Lerp(_initialScale, 
                _target.transform.localScale / 15, rate);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
