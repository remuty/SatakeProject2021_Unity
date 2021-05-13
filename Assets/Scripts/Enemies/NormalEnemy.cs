using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private EnemyGenerator _enemyGenerator;
    private Vector2[] _initialPosition;
    private Vector2[] _endPosition;
    private Vector2 _initialScale;
    private Vector2 _endScale;
    private int _lane;
    public int Lane
    {
        set { _lane = value; }
    }
    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        _enemyGenerator = GameObject.FindWithTag("EnemyGenerator").GetComponent<EnemyGenerator>();
        SetInitialPosition();
        SetEndPosition();
        _initialScale = new Vector2(1, 1);
        _endScale = new Vector2(4, 4);
        transform.position = _initialPosition[_lane];
        transform.localScale = _initialScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time < enemyData.speed)
        {
            _time += Time.deltaTime;
            var rate = _time / enemyData.speed;
            transform.position = Vector2.Lerp(_initialPosition[_lane], _endPosition[_lane], rate);
            transform.localScale = Vector2.Lerp(_initialScale, _endScale, rate);
        }
        else
        {
            Debug.Log(enemyData.atk + "ダメージ");//TODO:ダメージ処理
            _enemyGenerator.AddLane(_lane);
            Destroy(this.gameObject);
        }
    }

    void SetInitialPosition()
    {
        _initialPosition = new Vector2[3]
        {
            new Vector2(0,1),
            new Vector2(-1,1),
            new Vector2(1,1)
        };
    }

    void SetEndPosition()
    {
        _endPosition = new Vector2[3]
        {
            new Vector2(0,-2),
            new Vector2(-5,-2),
            new Vector2(5,-2)
        };
    }
}
