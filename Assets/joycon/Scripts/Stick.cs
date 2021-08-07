using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Linq;

public enum LorR
{
    Left,
    Right
}
public class Stick : MonoBehaviour
{

    private List<Joycon> joycons;
    [SerializeField]
    LorR joyconType;
    // Values made available via Unity
    private float[] stick;
    private Vector3 gyro;
    private Vector3 accel;
    private int jc_ind = 0;
    private Quaternion orientation;
    private Vector3 euler;
    bool pressed;
    //どれだけ強く振られたら判定するか
    public float shakeSpeed = -1;
    //どれだけ勢いよくしゃがんだら判定するか
    public float crouchSpeed = -3;
    //しゃがみ判定時間
    public float crouchCoolTime = 0.5f;
    //次に振れるようにするまでのクールタイム
    public float shakeCoolTime = 1f;
    //振られた瞬間true
    private ReactiveProperty<bool> _isShaked = new ReactiveProperty<bool>();

    public ReactiveProperty<bool> isShaked {
        get{
            return _isShaked;
        }
    }
    //しゃがんだらtrue
    private ReactiveProperty<bool> _isCrouch = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> isCrouch
    {
        get
        {
            return _isCrouch;
        }
    }
    //ジョイコンのボタン入力、センサの値等を管理しているクラス
    private Joycon _j;
    public Joycon j
    {
        get
        {
            return _j;
        }
    }
    
    
    void Start()
    {
        
        joycons = JoyconManager.Instance.j;
        switch (joyconType)
        {
            case LorR.Left:
                _j = joycons.Find(c => c.isLeft);
                break;
            case LorR.Right:
                _j = joycons.Find(c => !c.isLeft);
                break;
        }
        // _j = joycons[jc_ind];
        // if (joycons.Count < jc_ind + 1)
        // {
        //     Destroy(gameObject);
        // }
        //initShakeTime = shakeTime;
        
    }
    
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            if (!_isShaked.Value)
            {
                //振った判定
                if(accel.x <= shakeSpeed){
                    _isShaked.Value = true;
                    Invoke("SetIsShakedFalse", shakeCoolTime);
                }
            }
            stick = _j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = _j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = _j.GetAccel();


            orientation = _j.GetVector();

            euler = gameObject.transform.localEulerAngles;
            gameObject.transform.localRotation = new Quaternion(orientation.x, -orientation.z, orientation.y, -orientation.w);
            
            CrouchDecision();
        }
    }


    void SetIsShakedFalse()
    {
        _isShaked.Value = false;
    }
    

    void CrouchDecision()
    {
        if(gyro.y < crouchSpeed)
        {
            _isCrouch.Value = true;
            Invoke("SetIsCrouchFalse", crouchCoolTime);
        }
    }

    void SetIsCrouchFalse()
    {
        _isCrouch.Value = false;
    }
}