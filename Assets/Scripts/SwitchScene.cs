using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private GameObject titlePrefab;
    [SerializeField] private GameObject homePrefab;
    [SerializeField] private GameObject mainPrefab;
    private Stick _stick;
    private GameObject _title;
    private GameObject _home;
    private GameObject _main;

    private int _menuNum;

    public int MenuNum
    {
        set => _menuNum = value;
    }

    public enum Scenes
    {
        Title,
        Home,
        Main,
        Result
    }

    private Scenes _scene;

    public Scenes Scene
    {
        set => _scene = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
        _title = Instantiate(titlePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (_stick.j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            if (_scene == Scenes.Title)
            {
                if (_menuNum != -1)
                {
                    if (_menuNum == 0)
                    {
                        Destroy(_title);
                        _home = Instantiate(homePrefab);
                        _scene = Scenes.Home;
                    }
                    _menuNum = -1;
                }
                
            }
            else if (_scene == Scenes.Home)
            {
                if (_menuNum != -1)
                {
                    Destroy(_home);
                    if (_menuNum == 0)
                    {
                        LoadMain();
                    }
                    else if (_menuNum == 4)
                    {
                        _title = Instantiate(titlePrefab);
                        _scene = Scenes.Title;
                    }
                    _menuNum = -1;
                }
            }
            else if (_scene == Scenes.Result)
            {
                if (_menuNum != -1)
                {
                    Destroy(_main);
                    Time.timeScale = 1;
                    if (_menuNum == 0)
                    {
                        LoadMain();
                    }
                    else if (_menuNum == 1)
                    {
                        _home = Instantiate(homePrefab);
                        _scene = Scenes.Home;
                    }

                    _menuNum = -1;
                }
            }
        }
    }

    void LoadMain()
    {
        _main = Instantiate(mainPrefab);
        var canvases = _main.GetComponentsInChildren<Canvas>();
        foreach (var canvas in canvases)
        {
            canvas.worldCamera = Camera.main;
        }

        _scene = Scenes.Main;
    }
}