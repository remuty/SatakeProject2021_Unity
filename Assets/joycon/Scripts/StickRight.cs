using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StickRight : MonoBehaviour
{
    public Stick s;

    public Text successText;
    // Start is called before the first frame update
    void Start()
    {
        s.isShaked.Subscribe(n => {
            if(n){
                successText.text = "振った";
            } else {
                successText.text = "";
            }
        });
    }
    
    void Update()
    {

        //ジョイコンを震えさせることができるらしい
        if (s.j.GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            Debug.Log("Rumble");

            // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:

            s.j.SetRumble(160, 320, 0.6f, 200);

            // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
            // (Useful for dynamically changing rumble values.)
            // Then call SetRumble(0,0,0) when you want to turn it off.
        }

    }
}
