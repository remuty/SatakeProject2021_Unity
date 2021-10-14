using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTime : MonoBehaviour
{
    private SwitchScene _switchScene;
    private float _seconds;
    // Start is called before the first frame update
    void Start()
    {
        _switchScene = GameObject.FindWithTag("SwitchScene").GetComponent<SwitchScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_switchScene.Scene == SwitchScene.Scenes.Main)
        {
            _seconds += Time.deltaTime;
        }
        Debug.Log(_seconds);
    }
}
