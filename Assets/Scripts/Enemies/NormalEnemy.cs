using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private TransformData transformData;
    private EnemyGenerator _enemyGenerator;
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
        transform.position = transformData.initialPosition[_lane];
        transform.localScale = transformData.initialScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time < enemyData.speed)
        {
            _time += Time.deltaTime;
            var rate = _time / enemyData.speed;
            transform.position = Vector2.Lerp(transformData.initialPosition[_lane],
                transformData.endPosition[_lane], rate);
            transform.localScale = 
                Vector2.Lerp(transformData.initialScale, transformData.endScale, rate);
        }
        else
        {
            Debug.Log(enemyData.atk + "ダメージ");//TODO:ダメージ処理
            _enemyGenerator.AddLane(_lane);
            Destroy(this.gameObject);
        }
    }
}
