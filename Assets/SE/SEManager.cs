using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public enum Audio
    {
        Attack,
        Enemy,
        Battle,
        Result,
        UI
    }

    [SerializeField] private AudioClip[] _attackClips;
    [SerializeField] private AudioClip[] _enemyClips;
    [SerializeField] private AudioClip[] _battleClips;
    [SerializeField] private AudioClip[] _resultClips;
    [SerializeField] private AudioClip[] _uiClips;

    AudioSource[] _audio;
    private bool[] _isPlaying = {false, false, false, false, false};

    public bool[] IsPlaying
    {
        get => _isPlaying;
        set => _isPlaying = value;
    }

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
        if (!_isPlaying[i])
        {
            _isPlaying[i] = true;
            _audio[i].clip = _resultClips[0];
            _audio[i].Play();
        }
    }

    public void LevelUp()
    {
        var i = (int) Audio.Result;
        _isPlaying[i] = true;
        _audio[i].clip = _resultClips[2];
        _audio[i].Play();
    }

    public void GettingCard()
    {
        //遅らせて再生するために他のAudioSourceを適用
        _audio[0].clip = _resultClips[1];
        _audio[0].PlayDelayed(0.3f);
    }

    // UI Control
    //focus
    public void Cancel()
    {
        SE(14);
    }

    public void Focus()
    {
        SE(8);
    }

    public void Select()
    {
        SE(9);
    }
}