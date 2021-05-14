using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StickLeft : MonoBehaviour
{
    public Stick s;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //しゃがんだor立った瞬間実行される
        s.isCrouch.Subscribe(n =>
        {
            if(n == true)
            {
                anim.SetBool("isCrouch", true);
            } else
            {
                anim.SetBool("isCrouch", false);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (s.isCrouch.Value)
        {
            //しゃがんでいる間
        }
    }
}
