using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
    enum MenuAxis
    {
        horizontal,
        vertical
    }
    
    [SerializeField] private MenuAxis menuAxis;
    [SerializeField] private Text[] menu;
    
    private Stick _stick;
    private SwitchScene _switchScene;
    
    private int _menuNum = -1;
    private bool _isSelected;
    
    private Color defaultColor = new Color32(219, 219, 219, 65);
    private Color selectedColor = new Color32(219, 219, 219, 255);
    
    // Start is called before the first frame update
    void Start()
    {
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
        _switchScene.MenuNum = _menuNum;
        ChangeTextColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuAxis == MenuAxis.horizontal)
        {
            Select(_stick.j.GetStick()[0]); //スティック横軸
        }
        else if (menuAxis == MenuAxis.vertical)
        {
            Select(_stick.j.GetStick()[1]); //スティック縦軸
        }
    }

    void Select(float f)
    {
        if (Mathf.Abs(f) > 0.6f && !_isSelected) //joyconのスティックを傾けたとき
        {
            if (_menuNum == -1)
            {
                _menuNum = 0;
            }
            else if (f > 0)
            {
                _menuNum++;
            }
            else if (f < 0)
            {
                _menuNum--;
            }
        
            if (_menuNum < 0)
            {
                _menuNum = menu.Length -1;
            }
            else if (_menuNum > menu.Length -1)
            {
                _menuNum = 0;
            }

            ChangeTextColor();
            _isSelected = true;

            _switchScene.MenuNum = _menuNum;
        }
        else if (Mathf.Abs(f) < 0.2f && _isSelected)
        {
            _isSelected = false;
        }
    }

    void ChangeTextColor()  //テキストの色(透明度)を調整
    {
        foreach (var text in menu)
        {
            text.color = defaultColor;
        }
        if (_menuNum != -1)
        {
            menu[_menuNum].color = selectedColor;
        }
    }
}
