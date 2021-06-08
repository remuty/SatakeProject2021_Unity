using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Image[] sides;

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

    // Start is called before the first frame update
    void Start()
    {
        _rhythmManager = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>();
        _symbolCardDeck = GameObject.FindWithTag("SymbolCardDeck").GetComponent<SymbolCardDeck>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
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

                    if (_player.Target != null)
                    {
                        _player.Target.GetComponent<NormalEnemy>().AddDamage(_atk, _knockBackPower);
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
}