using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StickRight : MonoBehaviour
{
    //8方向を示す８個のCube
    public GameObject[] cube;
    //今振るべき方向はどこか管理する
    bool[] guideAngle = new bool[8];
    public Stick s;
    // Start is called before the first frame update
    void Start()
    {
        
        guideAngle[3] = true;

        //それぞれの値が変更されたときに１度だけ実行される
        //i.Indexで変更された値のindex
        //i.NewValueで変更後の値を取得できる

        //Joyconの角度が変わった時
        s.angle
            .ObserveReplace()
            .Subscribe(i =>
            {
                if (!s.isSelected)
                {
                    if (i.NewValue)
                    {
                        //今の角度に対する処理
                        cube[i.Index].GetComponent<Renderer>().material.color = Color.yellow;
                    }
                    else
                    {
                        //それ以外の角度に対する処理
                        if (guideAngle[i.Index])
                        {
                            cube[i.Index].GetComponent<Renderer>().material.color = Color.green;
                        } else
                        {
                            cube[i.Index].GetComponent<Renderer>().material.color = Color.white;
                        }
                        
                    }
                }
            });

        //ある角度が選ばれた時、Joyconのセレクトボタン（デフォルトでZR,ZL)が押された時
        s.selectedAngle
            .ObserveReplace()
            .Subscribe(i =>
            {
                if (i.NewValue)
                {
                    //選ばれている角度に対する処理
                    if (guideAngle[i.Index])
                    {
                        cube[i.Index].GetComponent<Renderer>().material.color = Color.blue;
                    } else
                    {
                        cube[i.Index].GetComponent<Renderer>().material.color = Color.red;
                    }
                    
                    
                }
                else
                {
                    //それ以外の角度に対する処理
                    cube[i.Index].GetComponent<Renderer>().material.color = Color.white;
                }
            });

        //Joyconが振られた時
        s.isShaked.Subscribe(n =>
        {
            //振られたときの処理
            if (n)
            {
                //s.SelectedNumが選ばれている角度のインデックスを保持している
                cube[s.selectedNum].GetComponent<Cube>().Slash();
            }
        });

    }
    
    void Update()
    {
        
        //それぞれの角度に毎フレーム処理をしたい時
        for (int i = 0; i < s.angle.Count; i++)
        {
            if (s.angle[i] && !s.isSelected)
            {
                //今の角度に対する処理
            }
            else if (s.selectedAngle[i])
            {
                //選ばれている角度に対する処理
            }
            else
            {
                //選ばれていない角度に対する処理
            }
        }

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
