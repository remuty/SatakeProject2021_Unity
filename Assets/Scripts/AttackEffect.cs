using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] private bool isFly;
    private float _time;
    private Vector2 _initialPosition;
    private Vector2 _initialScale;
    private Player _player;
    private SoundManager _sound;
    private int _atk;
    private float _knockBackPower;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _sound = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        _initialPosition = this.transform.position;
        _initialScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        var target = _player.Target;
        if (_time < 0.6f)
        {
            _time += Time.deltaTime;
            if (isFly)
            {
                var rate = _time / 0.6f;
                transform.position = Vector2.Lerp(_initialPosition,
                    target.transform.position, rate);
                transform.localScale = Vector2.Lerp(_initialScale,
                    target.transform.localScale / 4, rate);
                var pos = transform.position;
                pos.z = -1;
                transform.position = pos;
            }
            else
            {
                transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -1);
                transform.localScale = target.transform.localScale;
            }
        }
        else
        {
            _sound.StopAttack();
            if (target != null)
            {
                target.GetComponent<NormalEnemy>().AddDamage(_atk, _knockBackPower);
            }

            Destroy(this.gameObject);
        }
    }

    public void SetPower(int atk, float knockBackPower)
    {
        _atk = atk;
        _knockBackPower = knockBackPower;
    }
}