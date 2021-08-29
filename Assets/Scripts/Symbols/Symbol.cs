using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    enum Element
    {
        Default,
        Fire
    }

    [SerializeField] private Element element;
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private Image[] sides;
    [SerializeField] private GameObject[] guides;
    [SerializeField] private int _atk;
    [SerializeField] private float _knockBackPower;

    private RhythmManager _rhythmManager;
    private SymbolCardDeck _symbolCardDeck;
    private Player _player;

    private int _sideCount;

    private float _drawTime = 0.1f;
    private float _time;

    private bool _isSideDrawing;
    private bool _isSymbolDrawing;
    private bool _isAttacking;

    private Vector2 _initialPosition;
    private Vector2 _initialScale;

    // Start is called before the first frame update
    void Start()
    {
        _rhythmManager = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>();
        _symbolCardDeck = GameObject.FindWithTag("SymbolCardDeck").GetComponent<SymbolCardDeck>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _initialPosition = this.transform.position;
        _initialScale = this.transform.localScale;
        SwitchGuide();
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
                SwitchGuide();
                if (_sideCount >= sides.Length)
                {
                    if (_player.Target != null)
                    {
                        _isAttacking = true;
                        if (element != Element.Default)
                        {
                            var effect = Instantiate(attackEffect, this.transform.parent);
                            effect.GetComponent<AttackEffect>().Target = _player.Target;
                        }
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
            SwitchGuide();
        }

        if (_isAttacking) //攻撃処理
        {
            if (element == Element.Default)
            {
                if (_time < 0.3f)
                {
                    _time += Time.deltaTime;
                    var rate = _time / 0.3f;
                    transform.position = Vector2.Lerp(_initialPosition,
                        _player.Target.transform.position, rate);
                    transform.localScale = Vector2.Lerp(_initialScale,
                        _player.Target.transform.localScale / 15, rate);
                }
                else
                {
                    _player.Target.GetComponent<NormalEnemy>().AddDamage(_atk, _knockBackPower);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void DrawSymbol()
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

    void SwitchGuide()
    {
        foreach (var guide in guides)
        {
            guide.SetActive(false);
        }

        if (_sideCount < guides.Length)
        {
            guides[_sideCount].SetActive(true);
        }
    }
}