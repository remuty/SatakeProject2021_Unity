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

    private int _sideCount;
    
    private float _drawTime = 0.1f;

    private float _time;

    private bool _isDrawing;

    private GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        _rhythmManager = GameObject.FindWithTag("RhythmManager").GetComponent<RhythmManager>();
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _stick.isShaked.Subscribe(isShaked =>
        {
            //振られたときの処理
            if (_sideCount < sides.Length && isShaked)
            {
                if (_rhythmManager.CanBeat())
                {
                    _isDrawing = true;
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
        if (_isDrawing) //TODO:シンボルを描く処理
        {
            _time += Time.deltaTime;
        
            if (_time > _drawTime)
            {
                sides[_sideCount].fillAmount = 1;
                _isDrawing = false;
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
                }
            }
            else
            {
                sides[_sideCount].fillAmount = _time / _drawTime;
            }
        }

        if (_target == null || _stick.j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            SelectTarget();
        }
    }

    void SelectTarget()
    {
        SpriteRenderer[] renderers;
        if (_target != null)
        {
            renderers = _target.transform.Find("Outline").GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in renderers)
            {
                renderer.enabled = false;
            }
        }
        
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

        if (_target != null)
        {
            renderers = _target.transform.Find("Outline").GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
        }
    }
    void DrawSide()
    {
    }
}
