using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackObject : MonoBehaviour
{
    [SerializeField] private int atk;
    private float speed = 1.5f; //ノーツのスピードに合わせる
    private float _time;

    private Vector2 _initialPosition;
    private Vector2 _endPosition;
    private Vector2 _initialScale;
    private Vector2 _endScale;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _initialPosition = this.transform.position;
        _endPosition = new Vector2(0, 0);
        _initialScale = this.transform.localScale;
        _endScale = new Vector2(4, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (_time < speed)
        {
            _time += Time.deltaTime;
            var rate = _time / speed;
            transform.position =
                Vector3.Lerp(_initialPosition, _endPosition, rate * rate * rate);
            transform.localScale =
                Vector2.Lerp(_initialScale, _endScale, rate * rate * rate);
        }
        else
        {
            _player.AddDamage(atk, this.tag);
            Destroy(this.gameObject);
        }
    }
}