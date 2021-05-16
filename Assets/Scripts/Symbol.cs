using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Image[] _sides;
    
    private Stick _stick;

    private int _sideCount;
    
    private float _drawTime = 0.1f;

    private float _time;

    private bool _isDrawing;
    // Start is called before the first frame update
    void Start()
    {
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _stick.isShaked.Subscribe(isShaked =>
        {
            //振られたときの処理
            if (_sideCount < _sides.Length)
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
                _sides[_sideCount].fillAmount = 1;
                _isDrawing = false;
                _time = 0;
                _sideCount++;
            }
            else
            {
                _sides[_sideCount].fillAmount = _time / _drawTime;
            }
        }
    }

    void DrawSide()
    {
    }
}
