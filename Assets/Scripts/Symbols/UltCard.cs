using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltCard : MonoBehaviour
{
    [SerializeField] private Image ultGauge;
    [SerializeField] private Image ultFlash;
    [SerializeField] private Image background;
    [SerializeField] private Sprite ultSprite;
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private int _countMax;

    private SoundManager _sound;
    private GameObject _target;
    private float _count, _time;
    private float _timeMax = 0.6f;
    private bool _isMaxed, _isActivated;

    // Start is called before the first frame update
    void Start()
    {
        _sound = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_count >= _countMax && _isActivated)
        {
            _time += Time.deltaTime;
            if (_time < _timeMax)
            {
                var rate = _time / _timeMax;
                ultFlash.color = new Color(1, 1, 1, rate);
            }
            else if (_time >= _timeMax && !_isMaxed)
            {
                _isMaxed = true;
                ultFlash.color = new Color(1, 1, 1, 1);
                background.sprite = backgroundSprite;
                _sound.Astro();
                if (_target != null)
                {
                    //全ての敵を倒す
                    _target.GetComponent<NormalEnemy>().AddDamage(1000, 0);
                    var enemies = GameObject.FindGameObjectsWithTag("NormalEnemy");
                    foreach (var enemy in enemies)
                    {
                        enemy.GetComponent<NormalEnemy>().AddDamage(1000, 0);
                    }
                }
            }
            else if (_time < _timeMax * 1.5f)
            {
                var rate = _time / _timeMax * 1.5f;
                ultFlash.color = new Color(1, 1, 1, rate);
            }
            else
            {
                ultFlash.color = new Color(1, 1, 1, 0);
                _isMaxed = false;
                _isActivated = false;
                _count = 0;
                _time = 0;
                ultGauge.fillAmount = 0;
            }
        }
    }

    public void AddCount(int combo)
    {
        if (_count < _countMax)
        {
            _count += combo;
            if (_count >= _countMax)
            {
                _count = _countMax;
                _sound.Ult();
                background.sprite = ultSprite;
            }

            if (ultGauge != null)
            {
                ultGauge.fillAmount = _count / _countMax;
            }
        }
    }

    public void Activate(GameObject target)
    {
        if (!_isActivated)
        {
            _target = target;
            _isActivated = true;
        }
    }
}