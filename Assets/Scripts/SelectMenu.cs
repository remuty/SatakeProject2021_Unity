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
    [SerializeField] private Text[] menuTexts;
    [SerializeField] private Image[] menuImages;
    [SerializeField] private Color defaultColor = new Color32(160, 160, 160, 255);

    private Stick _stick;
    private SwitchScene _switchScene;

    private int _menuNum = -1;
    private bool _isSelected;

    private Color selectedColor = new Color32(255, 255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
        _switchScene.MenuNum = _menuNum;
        ChangeColor();
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

            if (menuTexts.Length != 0)
            {
                if (_menuNum < 0)
                {
                    _menuNum = menuTexts.Length - 1;
                }
                else if (_menuNum > menuTexts.Length - 1)
                {
                    _menuNum = 0;
                }
            }
            else
            {
                if (_menuNum < 0)
                {
                    _menuNum = menuImages.Length - 1;
                }
                else if (_menuNum > menuImages.Length - 1)
                {
                    _menuNum = 0;
                }
            }

            ChangeColor();
            _isSelected = true;

            _switchScene.MenuNum = _menuNum;
        }
        else if (Mathf.Abs(f) < 0.2f && _isSelected)
        {
            _isSelected = false;
        }
    }

    void ChangeColor() //メニューの色(透明度)を調整
    {
        if (menuTexts.Length != 0)
        {
            foreach (var text in menuTexts)
            {
                text.color = defaultColor;
            }

            if (_menuNum != -1)
            {
                menuTexts[_menuNum].color = selectedColor;
            }
        }
        else if (menuImages.Length != 0)
        {
            foreach (var image in menuImages)
            {
                image.color = defaultColor;
            }

            if (_menuNum != -1)
            {
                menuImages[_menuNum].color = selectedColor;
            }
        }
    }
}