using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    private Stick _stick;

    public enum Scenes
    {
        Title,
        Main
    }

    private static Scenes _scenes;

    // Start is called before the first frame update
    void Start()
    {
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_stick.j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            if (_scenes == Scenes.Title)
            {
                SceneManager.LoadScene("Main");
                _scenes = Scenes.Main;
            }
            else if (_scenes == Scenes.Main)
            {
                SceneManager.LoadScene("Title");
                _scenes = Scenes.Title;
                Time.timeScale = 1;
            }
        }
    }
}