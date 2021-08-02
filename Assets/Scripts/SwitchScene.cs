using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private GameObject titlePrefab;
    [SerializeField] private GameObject mainPrefab;
    private Stick _stick;
    private GameObject _title;
    private GameObject _main;

    public enum Scenes
    {
        Title,
        Main,
        Result
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
        if (_stick.j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            if (_scene == Scenes.Title)
            {
                Destroy(_title);
                _main = Instantiate(mainPrefab);
                var canvases = _main.GetComponentsInChildren<Canvas>();
                foreach (var canvas in canvases)
                {
                    canvas.worldCamera = Camera.main;
                }
                _scene = Scenes.Main;
            }
            else if (_scene == Scenes.Result)
            {
                Debug.Log(titlePrefab);
                Time.timeScale = 1;
                Destroy(_main);
                _title = Instantiate(titlePrefab);
                _scene = Scenes.Title;
            }
        }
    }
}