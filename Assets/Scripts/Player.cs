using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider hpGauge;

    [SerializeField] private int maxHp;

    private int _hp;
    
    private Stick _stick;
    
    private SymbolCardDeck _symbolCardDeck;
    
    private GameObject _target;

    public GameObject Target => _target;

    // Start is called before the first frame update
    void Start()
    {
        hpGauge.value = 1;
        _hp = maxHp;
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _symbolCardDeck = GameObject.FindWithTag("SymbolCardDeck").GetComponent<SymbolCardDeck>();
        _stick.isShaked.Subscribe(isShaked =>
        {
            var symbol = _symbolCardDeck.SelectedCard.Symbol;
            //振られたときの処理
            if (isShaked)
            {
                symbol.DrawSymbol();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null || _stick.j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            SelectTarget();
        }
    }
    
    public void AddDamage(int damage)
    {
        _hp -= damage;
        hpGauge.value = (float)_hp / (float)maxHp;
    }
    
    void SelectTarget()
    {
        SwitchRenderer(false);
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

        SwitchRenderer(true);
    }

    void SwitchRenderer(bool b)
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
