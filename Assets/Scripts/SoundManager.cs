using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Audio
    {
        Attack,
        Enemy,
        Battle,
        Result,
        UI,
        BGM
    }

    [SerializeField] private AudioClip[] _attackClips;
    [SerializeField] private AudioClip[] _enemyClips;
    [SerializeField] private AudioClip[] _battleClips;
    [SerializeField] private AudioClip[] _resultClips;
    [SerializeField] private AudioClip[] _uiClips;

    AudioSource[] _audio;
    private bool[] _isPlayed = {false, false, false, false, false};

    // Use this for initialization
    void Start()
    {
        _audio = GetComponents<AudioSource>();
    }

    // Battle Screen
    // Attack
    void SE(int num)
    {
        _audio[num].PlayOneShot(_audio[num].clip);
    }

    public void Lightning()
    {
        SE(0);
    }

    public void Astro()
    {
        SE(1);
    }

    public void Ice()
    {
        SE(2);
    }

    public void Normal()
    {
        SE(3);
    }

    public void Rock()
    {
        SE(4);
    }

    public void Wind()
    {
        SE(5);
    }

    // Enemy
    public void Spawn()
    {
        SE(6);
    }

    public void Kill()
    {
        SE(7);
    }

    // Other
    public void Ult()
    {
        SE(15);
    }

    public void Alert()
    {
        SE(12);
    }

    public void Damage()
    {
        SE(13);
    }

    public void PhaseUp()
    {
        SE(16);
    }

    // Result Screen
    public void ExpUp()
    {
        var i = (int) Audio.Result;
        if (!_isPlayed[i])
        {
            _isPlayed[i] = true;
            _audio[i].clip = _resultClips[0];
            _audio[i].Play();
        }
    }

    public void LevelUp()
    {
        var i = (int) Audio.Result;
        _isPlayed[i] = true;
        _audio[i].clip = _resultClips[1];
        _audio[i].Play();
    }

    public void GettingCard()
    {
        //遅らせて再生するために他のAudioSourceを適用
        _audio[0].clip = _resultClips[2];
        _audio[0].PlayDelayed(0.3f);
    }

    // UI Control
    //focus
    public void Cancel()
    {
        _audio[(int)Audio.UI].PlayOneShot(_uiClips[0]);
    }

    public void Focus()
    {
        _audio[(int)Audio.UI].PlayOneShot(_uiClips[1]);
    }

    public void Select()
    {
        _audio[(int)Audio.UI].PlayOneShot(_uiClips[2]);
    }

    //BGM
    public void PlayBGM()
    {
        _audio[(int) Audio.BGM].Play();
    }

    public void StopBGM()
    {
        _audio[(int) Audio.BGM].Stop();
    }
}