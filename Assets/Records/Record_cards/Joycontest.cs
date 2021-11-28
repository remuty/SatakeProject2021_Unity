using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joycontest : MonoBehaviour
{

    private Stick _stick;
    public Scrollbar _scrollbar;

    private void Start()
    {
        _stick = GameObject.FindWithTag("JoyConRight").GetComponent<Stick>();
       
    }

   
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_scrollbar);
        //Debug.Log(_scrollbar.value);
        //Debug.Log(_stick.j.GetStick()[1]);
        if (_stick.j.GetStick()[1] > 0f)
        {
            Debug.Log("卍");
            _scrollbar.value += 0.01f;
        }
        else if (_stick.j.GetStick()[1] < 0f)
        {
            _scrollbar.value -= 0.01f;
        }
    }
}
