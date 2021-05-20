using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Image[] sides;
    
    private Stick _stick;

    private int _sideCount;
    
    private float _drawTime = 0.1f;

    private float _time;

    private bool _isDrawing;

    private float _knockBackPower = 2;

    private GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _stick.isShaked.Subscribe(isShaked =>
        {
            //振られたときの処理
            if (_sideCount < sides.Length)
            {
                if (isShaked)
                {
                    _isDrawing = true;
                    // _sides[_sideCount].fillAmount = 1;
                    // _sideCount++;
                    //s.SelectedNumが選ばれている角度のインデックスを保持している
                    //cube[s.selectedNum].GetComponent<Cube>().Slash();
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDrawing)
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
                    _target.GetComponent<NormalEnemy>().AddDamage(_knockBackPower);
                }
            }
            else
            {
                sides[_sideCount].fillAmount = _time / _drawTime;
            }
        }

        if (_target == null)
        {
            SelectTarget();
        }
    }

    void SelectTarget()
    {
        _target = GameObject.FindWithTag("NormalEnemy");
        var renderers = _target.transform.Find("Outline").GetComponentsInChildren<SpriteRenderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = true;
        }
    }
    void DrawSide()
    {
    }
}
