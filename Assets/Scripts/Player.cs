using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _resultCanvas;
    [SerializeField] private Image _barrier;
    [SerializeField] private Slider hpGauge;
    [SerializeField] private int maxHp;

    private int _hp;

    private Stick _stickR;
    private Stick _stickL;

    private SymbolCardDeck _symbolCardDeck;

    private SwitchScene _switchScene;

    private GameObject _target;

    public GameObject Target => _target;

    private bool _isSelected;
    private bool _isCrouch;
    public bool IsCrouch => _isCrouch;

    // Start is called before the first frame update
    void Start()
    {
        _resultCanvas.SetActive(false);
        _stickR = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _stickL = GameObject.FindWithTag("JoyConLeft").GetComponent<Stick>();
        _symbolCardDeck = GameObject.FindWithTag("SymbolCardDeck").GetComponent<SymbolCardDeck>();
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
        hpGauge.value = 1;
        _hp = maxHp;
        _stickR.isShaked.Subscribe(n =>
        {
            //振られたときの処理
            if (n && _symbolCardDeck != null)
            {
                var symbol = _symbolCardDeck.SelectedCard.Symbol;
                symbol.DrawSymbol();
            }
        });
        //しゃがんだor立った瞬間実行される
        _stickL.isCrouch.Subscribe(n =>
        {
            if (n)
            {
                _isCrouch = true;
                _barrier.enabled = true;
            }
            else
            {
                _isCrouch = false;
                _barrier.enabled = false;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null || _stickR.j.GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            SelectTarget();
        }

        if (Mathf.Abs(_stickR.j.GetStick()[0]) > 0.6f && !_isSelected) //joyconのスティックを左右に傾けたとき
        {
            if (_symbolCardDeck == null)
            {
                _symbolCardDeck = GameObject.FindWithTag("SymbolCardDeck").GetComponent<SymbolCardDeck>();
            }
            _symbolCardDeck.SelectCard(_stickR.j.GetStick()[0]);
            _isSelected = true;
        }
        else if (Mathf.Abs(_stickR.j.GetStick()[0]) < 0.2f && _isSelected)
        {
            _isSelected = false;
        }

        if (_hp <= 0)
        {
            _resultCanvas.SetActive(true);
            _switchScene.Scene = SwitchScene.Scenes.Result0;
            Time.timeScale = 0;
            GetComponent<Player>().enabled = false;
        }
    }

    public void AddDamage(int damage,string tag)
    {
        //敵の攻撃オブジェクトをしゃがみで避けたときだけダメージを受けない
        if (tag != "EnemyAttackObject" || !_isCrouch)
        {
            _hp -= damage;
            hpGauge.value = (float) _hp / (float) maxHp;
        }
    }

    void SelectTarget()
    {
        // SwitchOutline(false);
        var enemies = GameObject.FindGameObjectsWithTag("NormalEnemy");
        var targetPosY = 1f;
        foreach (var enemy in enemies)
        {
            if (enemy.transform.position.y < targetPosY)
            {
                _target = enemy;
                targetPosY = enemy.transform.position.y;
            }
        }

        _target.tag = "Target";
        // SwitchOutline(true);
    }

    void SwitchOutline(bool b)
    {
        if (_target != null)
        {
            var renderers = _target.transform.Find("Outline").GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in renderers)
            {
                renderer.enabled = b;
            }
        }
    }
}