using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    private Vector2[] _initialPosition;
    private Vector2[] _endPosition;
    private Vector2 _initialScale;
    private Vector2 _endScale;
    [SerializeField] private int _lane;
    private float _time;

    public int Lane
    {
        set { _lane = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetInitialPosition();
        SetEndPosition();
        _initialScale = new Vector2(1, 1);
        _endScale = new Vector2(2, 2);
        transform.position = _initialPosition[_lane];
        transform.localScale = _initialScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time < _enemyData.speed)
        {
            _time += Time.deltaTime;
            var rate = _time / _enemyData.speed;
            transform.position = Vector2.Lerp(_initialPosition[_lane], _endPosition[_lane], rate);
            transform.localScale = Vector2.Lerp(_initialScale, _endScale, rate);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void SetInitialPosition()
    {
        _initialPosition = new Vector2[3]
        {
            new Vector2(0,0),
            new Vector2(-2f,0),
            new Vector2(2f,0)
        };
    }

    void SetEndPosition()
    {
        _endPosition = new Vector2[3]
        {
            new Vector2(0,-2),
            new Vector2(-4,-2),
            new Vector2(4,-2)
        };
    }
}
