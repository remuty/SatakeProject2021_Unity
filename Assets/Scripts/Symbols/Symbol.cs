using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Image[] sides;
    [SerializeField] private GameObject[] guides;
    [SerializeField] private int _atk;
    [SerializeField] private float _knockBackPower;

    private SymbolCard.Element _element;
    private GameObject _attackEffect;

    private RhythmManager _rhythmManager;
    private SymbolCardDeck _symbolCardDeck;
    private Player _player;
    private SoundManager _sound;

    private int _sideCount;
    private float _drawTime = 0.05f;
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
        _sound = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        _initialPosition = this.transform.position;
        _initialScale = this.transform.localScale;
        SwitchGuide();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSideDrawing)
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
                        if (_element != SymbolCard.Element.Default)
                        {
                            _sound.AttackEffect(_element);
                            var effect = Instantiate(_attackEffect);
                            effect.GetComponent<AttackEffect>().SetPower(_atk, _knockBackPower);
                        }
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }

                    _isSymbolDrawing = false;
                    _symbolCardDeck.DrawCard();
                }
            }
            else
            {
                sides[_sideCount].fillAmount = _time / _drawTime;
            }
        }

        if (_isSymbolDrawing)
        {
            _rhythmManager.NotesCheck(); //シンボルを描く途中でノーツを見逃したらコンボリセット
            if (_rhythmManager.Combo == 0) //コンボがゼロになったらシンボルリセット
            {
                for (int i = 0; i < sides.Length; i++)
                {
                    sides[i].fillAmount = 0;
                    _sideCount = 0;
                }

                SwitchGuide();
                _isSideDrawing = false;
                _isSymbolDrawing = false;
            }
        }

        if (_isAttacking) //攻撃処理
        {
            if (_element == SymbolCard.Element.Default)
            {
                if (_time < 0.3f)
                {
                    _sound.AttackNormal();
                    _time += Time.deltaTime;
                    var rate = _time / 0.3f;
                    transform.position = Vector2.Lerp(_initialPosition,
                        _player.Target.transform.position, rate);
                    transform.localScale = Vector2.Lerp(_initialScale,
                        _player.Target.transform.localScale / 15, rate);
                }
                else
                {
                    _sound.StopAttack();
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
        if (_rhythmManager.CanBeat() == RhythmManager.Beat.good)
        {
            _isSideDrawing = true;
            _isSymbolDrawing = true;
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

    public void SetElement(SymbolCard.Element element, GameObject attackEffect)
    {
        _element = element;
        _attackEffect = attackEffect;
    }
}