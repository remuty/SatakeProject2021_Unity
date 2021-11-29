using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private GameObject titlePrefab;
    [SerializeField] private GameObject homePrefab;
    [SerializeField] private GameObject recordPrefab;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject mainPrefab;
    private Stick _stick;
    private GameObject _current;

    private int _menuNum;

    public int MenuNum
    {
        set => _menuNum = value;
    }

    public enum Scenes
    {
        Title,
        Home,
        Record,
        Card,
        Main,
        Result0,
        Result1,
        Result2
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
        _current = Instantiate(titlePrefab);
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
                    Load(homePrefab,Scenes.Home);
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
                if (_menuNum == 0)  //スタートボタン
                {
                    LoadMain();
                }
                else if (_menuNum == 1) //手札ボタン
                {
                    Load(cardPrefab,Scenes.Card);
                }
                else if (_menuNum == 2) //記録ボタン
                {
                    Load(recordPrefab,Scenes.Record);
                }
                else if (_menuNum == 4) //やめるボタン
                {
                    //アプリ終了
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
            else if (_scene == Scenes.Record)
            {
                if (_menuNum == 0)  //戻るボタン
                {
                    Load(homePrefab,Scenes.Home);
                }
                else if (_menuNum == 1) //所持札一覧ボタン
                {
                    Load(cardPrefab,Scenes.Card);
                }
            }
            else if (_scene == Scenes.Card)
            {
                if (_menuNum == 0)  //戻るボタン
                {
                    Load(homePrefab,Scenes.Home);
                }
                else if (_menuNum == 1) //記録一覧ボタン
                {
                    Load(recordPrefab,Scenes.Record);
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
                Time.timeScale = 1;
                if (_menuNum == 0)
                {
                    LoadMain();
                }
                else if (_menuNum == 1)
                {
                    Load(homePrefab,Scenes.Home);
                }
            }
            else if (_scene == Scenes.Result2)
            {
                if (_menuNum == 0)
                {
                    GameObject.Find("ResultPanel").transform.Find("Result1").gameObject.SetActive(true);
                    _scene = Scenes.Result1;
                    GameObject.Find("Result2").SetActive(false);
                }
            }

            _menuNum = -1;
        }
    }

    void LoadMain()
    {
        Destroy(_current);
        _current = Instantiate(mainPrefab);
        var canvases = _current.GetComponentsInChildren<Canvas>();
        foreach (var canvas in canvases)
        {
            canvas.worldCamera = Camera.main;
        }

        _scene = Scenes.Main;
    }

    void Load(GameObject next,Scenes scene)
    {
        Destroy(_current);
        _current = Instantiate(next);
        _scene = scene;
    }
}