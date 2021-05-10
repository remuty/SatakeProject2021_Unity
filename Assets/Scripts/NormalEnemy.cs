using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    private Vector2[] _initialPosition;
    private Vector2[] _endPosition;
    private Vector2 _endScale;
    private int _lane;

    public int Lane
    {
        set { _lane = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetInitialPosition();
        SetEndPosition();
        _endScale = new Vector2(2, 2);
        transform.position = _initialPosition[_lane];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetInitialPosition()
    {
        _initialPosition = new Vector2[3]
        {
            new Vector2(-1.5f,0),
            new Vector2(0,0),
            new Vector2(1.5f,0)
        };
    }

    void SetEndPosition()
    {
        _endPosition = new Vector2[3]
        {
            new Vector2(-4,-2),
            new Vector2(0,-2),
            new Vector2(4,-2)
        };
    }
}
