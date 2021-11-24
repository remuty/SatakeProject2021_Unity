using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private TransformData transformData;
    [SerializeField] private GameObject atkObjPrefab;
    [SerializeField] private bool canAttack;

    private EnemyGenerator _enemyGenerator;
    private Player _player;
    private Record _record;
    private int _lane;

    public int Lane
    {
        set => _lane = value;
    }

    private float _moveTime;
    private float _atkTime; //攻撃までの時間を計る
    private float _attackTime; //攻撃するタイミングの時間
    private int _hp;

    // Start is called before the first frame update
    void Start()
    {
        _enemyGenerator = GameObject.FindWithTag("EnemyGenerator").GetComponent<EnemyGenerator>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _record = GameObject.FindWithTag("Record").GetComponent<Record>();
        transform.position = transformData.initialPosition[_lane];
        transform.localScale = transformData.initialScale;
        _hp = enemyData.maxHp;
        _attackTime = Random.Range(enemyData.speed / 5, enemyData.speed * 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveTime < enemyData.speed)
        {
            //近づいてくる
            _moveTime += Time.deltaTime;
            var rate = _moveTime / enemyData.speed;
            transform.position = Vector3.Lerp(transformData.initialPosition[_lane],
                transformData.endPosition[_lane], rate * rate * rate);
            transform.localScale =
                Vector2.Lerp(transformData.initialScale, transformData.endScale, rate * rate * rate);
        }
        else
        {
            //到達したらプレイヤーに攻撃
            _player.AddDamage(enemyData.atk, this.tag);
            _enemyGenerator.AddLane(_lane);
            Destroy(this.gameObject);
        }

        if (_hp <= 0)
        {
            //倒されたらスコアに加算
            _enemyGenerator.AddLane(_lane);
            _record.AddScore(enemyData.point);
            Destroy(this.gameObject);
        }

        if (canAttack)
        {
            _atkTime += Time.deltaTime;
            //1番前にいる敵だけが攻撃できる
            if (_atkTime > _attackTime && this.CompareTag("Target"))
            {
                Instantiate(atkObjPrefab, this.transform.position, Quaternion.identity, this.transform.parent);
                _atkTime = 0;
                _attackTime = Random.Range(enemyData.speed / 2, enemyData.speed * 2);
            }
        }
    }

    public void AddDamage(int damage, float knockBack)
    {
        _hp -= damage;
        _moveTime -= knockBack;
    }
}