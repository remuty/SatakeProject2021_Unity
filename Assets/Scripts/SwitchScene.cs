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
        Result0,
        Result1
    }

    private Scenes _scene;

    public Scenes Scene
    {
        get => _scene;
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
        if (_stick.j.GetButtonDown(Joycon.Button.DPAD_RIGHT) && _menuNum != -1)
        {
            if (_scene == Scenes.Title)
            {
                if (_menuNum == 0)
                {
                    Destroy(_title);
                    _home = Instantiate(homePrefab);
                    _scene = Scenes.Home;
                }
                else if (_menuNum == 1)
                {
                    //アプリ終了
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
            else if (_scene == Scenes.Home)
            {
                if (_menuNum == 0)
                {
                    Destroy(_home);
                    LoadMain();
                }
                else if (_menuNum == 4)
                {
                    //アプリ終了
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
            else if (_scene == Scenes.Result0)
            {
                if (_menuNum == 0)
                {
                    GameObject.Find("ResultPanel").transform.Find("Result1").gameObject.SetActive(true);
                    _scene = Scenes.Result1;
                    GameObject.Find("Result0").SetActive(false);
                }
            }
            else if (_scene == Scenes.Result1)
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
            }

            _menuNum = -1;
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