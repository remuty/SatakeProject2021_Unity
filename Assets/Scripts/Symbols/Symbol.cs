using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Image[] sides;

    [SerializeField] private int _atk;

    [SerializeField] private float _knockBackPower;

    private Stick _stick;

    private RhythmManager _rhythmManager;

    private SymbolCardDeck _symbolCardDeck;

    private int _sideCount;

    private float _drawTime = 0.1f;

    private float _time;

    private bool _isSideDrawing;

    private bool _isSymbolDrawing;

    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"start{this.name}");
        _rhythmManager = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>();
        _symbolCardDeck = GameObject.FindWithTag("SymbolCardDeck").GetComponent<SymbolCardDeck>();
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _stick.isShaked.Subscribe(isShaked =>
        {
            //振られたときの処理
            if (_sideCount < sides.Length && isShaked)
            {
                if (_rhythmManager.CanBeat())
                {
                    _isSideDrawing = true;
                    _isSymbolDrawing = true;
                }
                else
                {
                    for (int i = 0; i < sides.Length; i++)
                    {
                        sides[i].fillAmount = 0;
                        _sideCount = 0;
                    }
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSideDrawing) //TODO:シンボルを描く処理
        {
            
            _time += Time.deltaTime;
            if (_time > _drawTime)
            {
                sides[_sideCount].fillAmount = 1;
                _isSideDrawing = false;
                _time = 0;
                _sideCount++;
                if (_sideCount >= sides.Length)
                {
                    for (int i = 0; i < sides.Length; i++)
                    {
                        sides[i].fillAmount = 0;
                        _sideCount = 0;
                    }

                    if (_target != null)
                    {
                        _target.GetComponent<NormalEnemy>().AddDamage(_atk, _knockBackPower);
                    }
                    
                    _symbolCardDeck.DrawCard();
                }
            }
            else
            {
                sides[_sideCount].fillAmount = _time / _drawTime;
            }
        }

        if (_isSymbolDrawing && _rhythmManager.Combo == 0)
        {
            for (int i = 0; i < sides.Length; i++)
            {
                sides[i].fillAmount = 0;
                _sideCount = 0;
            }
        }

        if (_target == null || _stick.j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            SelectTarget();
        }
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