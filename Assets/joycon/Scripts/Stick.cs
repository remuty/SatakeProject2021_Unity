using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;


public class Stick : MonoBehaviour
{

    public List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    //右なら０左なら１
    public int jc_ind = 0;
    public Quaternion orientation;
    public Vector3 euler;
    bool pressed;

    //どれだけ強く振られたら判定するか
    public float shakeSpeed = -1;
    public float crouchSpeed = -3;
    //しゃがみ判定時間
    public float crouchTime = 0.5f;
    //次に振れるようにするまでのクールタイム
    public float coolTime = 1f;
    //真ん中上から反時計回りに８方向
    //今の傾きのみtrue
    public ReactiveCollection<bool> angle = new ReactiveCollection<bool>();
    //選ばれている向きのみtrue
    public ReactiveCollection<bool> selectedAngle = new ReactiveCollection<bool>();
    public int selectedNum;
    //１つでも選ばれているものがあればtrue
    public bool isSelected;
    //振られた瞬間true
    public ReactiveProperty<bool> isShaked = new ReactiveProperty<bool>();
    //しゃがんだらtrue
    public ReactiveProperty<bool> isCrouch = new ReactiveProperty<bool>();
    //ジョイコンのボタン入力、センサの値等を管理しているクラス
    public Joycon j { get; set; }
    //選ぶボタン、リセットするボタン
    //Inspectorビューから指定できる
    public Joycon.Button selectButton = Joycon.Button.SHOULDER_2;
    public Joycon.Button recenterButton = Joycon.Button.SHOULDER_1;
    
    void Start()
    {
        
        joycons = JoyconManager.Instance.j;
        j = joycons[jc_ind];
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < 8; i++)
        {
            angle.Add(false);
            selectedAngle.Add(false);
        }
    }
    
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            
            // GetButtonDown checks if a button has been pressed (not held)

            if (j.GetButtonDown(selectButton))
            {
                pressed = true;
            }
            // GetButtonDown checks if a button has been released
            if (j.GetButtonUp(selectButton))
            {
                //Debug.Log ("Shoulder button 2 up");
                pressed = false;
                for (int i = 0; i < selectedAngle.Count; i++)
                {
                    selectedAngle[i] = false;
                }
            }
            // GetButtonDown checks if a button is currently down (pressed or held)
            if (j.GetButton(selectButton))
            {
                //振る方向を判定するかどうか（今はしていない）
                if (accel.x <= shakeSpeed && !isShaked.Value)
                {
                    isShaked.Value = true;
                    Invoke("SetIsShakedFalse", coolTime);
                }
            }


            if (j.GetButtonDown(recenterButton))
            {
                j.Recenter();
            }
            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();


            orientation = j.GetVector();


            gameObject.transform.localRotation = new Quaternion(orientation.x, -orientation.z, orientation.y, -orientation.w);

            euler = gameObject.transform.localEulerAngles;

            TiltDecision();
            CrouchDecision();
        }
    }


    void SetIsShakedFalse()
    {
        isShaked.Value = false;
    }

   //傾き判定そのうち改善したい
    void TiltDecision()
    {
        isSelected = selectedAngle.Any(n => n);
        if (euler.z >= 157.5 && euler.z < 202.5)
        {
            AngleDecision(0);
        }
        else if (euler.z >= 112.5 && euler.z < 157.5)
        {
            AngleDecision(1);
        }
        else if (euler.z >= 67.5 && euler.z < 112.5)
        {
            AngleDecision(2);
        }
        else if (euler.z >= 22.5 && euler.z < 67.5)
        {
            AngleDecision(3);

        }
        else if (euler.z >= 202.5 && euler.z < 247.5)
        {
            AngleDecision(7);
        }
        else if (euler.z >= 247.5 && euler.z < 297.5)
        {
            AngleDecision(6);
        }
        else if (euler.z >= 297.5 && euler.z < 342.5)
        {
            AngleDecision(5);
        }
        else
        {
            AngleDecision(4);
        }
    }

    void AngleDecision(int num)
    {
        for (int i = 0; i < angle.Count; i++)
        {
            if (i == num)
            {
                angle[i] = true;
                if (pressed && !isSelected)
                {
                    selectedAngle[i] = true;
                    selectedNum = i;
                }
            }
            else
            {
                angle[i] = false;
            }
        }
    }

    void CrouchDecision()
    {
        if(gyro.y < crouchSpeed)
        {
            isCrouch.Value = true;
            Invoke("SetIsCrouchFalse", crouchTime);
        }
    }

    void SetIsCrouchFalse()
    {
        isCrouch.Value = false;
    }
}