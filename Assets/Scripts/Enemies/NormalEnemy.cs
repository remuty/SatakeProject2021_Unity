using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private TransformData transformData;
    private EnemyGenerator _enemyGenerator;
    private Player _player;
    private int _lane;
    public int Lane
    {
        set { _lane = value; }
    }
    private float _time;
    private int _hp;

    // Start is called before the first frame update
    void Start()
    {
        _enemyGenerator = GameObject.FindWithTag("EnemyGenerator").GetComponent<EnemyGenerator>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        transform.position = transformData.initialPosition[_lane];
        transform.localScale = transformData.initialScale;
        _hp = enemyData.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time < enemyData.speed)
        {
            _time += Time.deltaTime;
            var rate = _time / enemyData.speed;
            transform.position = Vector3.Lerp(transformData.initialPosition[_lane],
                transformData.endPosition[_lane], rate * rate);
            transform.localScale = 
                Vector2.Lerp(transformData.initialScale, transformData.endScale, rate * rate);
        }
        else
        {
            _player.AddDamage(enemyData.atk);
            _enemyGenerator.AddLane(_lane);
            Destroy(this.gameObject);
        }

        if (_hp <= 0)
        {
            _enemyGenerator.AddLane(_lane);
            Destroy(this.gameObject);
        }
    }

    public void AddDamage(float knockBackPower)  //TODO:ダメージを受ける処理
    {
        _time -= knockBackPower;
        Debug.Log(knockBackPower + "ノックバック");
    }
}
