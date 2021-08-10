using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _resultCanvas;

    [SerializeField] private Slider hpGauge;

    [SerializeField] private int maxHp;

    private int _hp;

    private Stick _stick;

    private SymbolCardDeck _symbolCardDeck;
    
    private SwitchScene _switchScene;

    private GameObject _target;

    public GameObject Target => _target;

    private bool _isSelected;

    // Start is called before the first frame update
    void Start()
    {
        _resultCanvas.SetActive(false);
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _symbolCardDeck = GameObject.FindWithTag("SymbolCardDeck").GetComponent<SymbolCardDeck>();
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
        hpGauge.value = 1;
        _hp = maxHp;
        _stick.isShaked.Subscribe(isShaked =>
        {
            //振られたときの処理
            if (isShaked && _symbolCardDeck != null)
            {
                var symbol = _symbolCardDeck.SelectedCard.Symbol;
                symbol.DrawSymbol();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null || _stick.j.GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            SelectTarget();
        }

        if (Mathf.Abs(_stick.j.GetStick()[0]) > 0.6f && !_isSelected) //joyconのスティックを左右に傾けたとき
        {
            _symbolCardDeck.SelectCard(_stick.j.GetStick()[0]);
            _isSelected = true;
        }
        else if (Mathf.Abs(_stick.j.GetStick()[0]) < 0.2f && _isSelected)
        {
            _isSelected = false;
        }

        if (_hp <= 0)
        {
            _resultCanvas.SetActive(true);
            _switchScene.Scene = SwitchScene.Scenes.Result;
            Time.timeScale = 0;
            this.gameObject.SetActive(false);
        }
    }

    public void AddDamage(int damage)
    {
        _hp -= damage;
        hpGauge.value = (float) _hp / (float) maxHp;
    }

    void SelectTarget()
    {
        SwitchOutline(false);
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

        SwitchOutline(true);
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