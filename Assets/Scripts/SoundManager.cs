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
    public void AttackEffect(SymbolCard.Element element)
    {
        var i = (int) Audio.Attack;
        if (!_isPlayed[i])
        {
            _isPlayed[i] = true;
            switch (element)
            {
                case SymbolCard.Element.Ice:
                    _audio[i].PlayOneShot(_attackClips[1]);
                    // _audio[i].clip = _attackClips[1];
                    // _audio[i].Play();
                    break;
                case SymbolCard.Element.Lightning:
                    _audio[i].PlayOneShot(_attackClips[2]);
                    // _audio[i].clip = _attackClips[2];
                    // _audio[i].Play();
                    break;
                case SymbolCard.Element.Rock:
                    _audio[i].PlayOneShot(_attackClips[3]);
                    // _audio[i].clip = _attackClips[3];
                    // _audio[i].Play();
                    break;
                case SymbolCard.Element.Wind:
                    _audio[i].PlayOneShot(_attackClips[4]);
                    // _audio[i].clip = _attackClips[4];
                    // _audio[i].Play();
                    break;
                default:
                    _audio[i].PlayOneShot(_attackClips[0]);
                    // _audio[i].clip = _attackClips[0];
                    // _audio[i].Play();
                    break;
            }
        }
        
    }

    public void AttackNormal()
    {
        var i = (int) Audio.Attack;
        if (!_isPlayed[i])
        {
            _isPlayed[i] = true;
            _audio[i].PlayOneShot(_attackClips[0]);
        }
    }

    public void StopAttack()
    {
        var i = (int) Audio.Attack;
        _isPlayed[i] = false;
        // _audio[i].Stop();
    }

    // Enemy
    public void Spawn()
    {
        _audio[(int) Audio.Enemy].PlayOneShot(_enemyClips[0]);
    }

    public void Kill()
    {
        _audio[(int) Audio.Enemy].PlayOneShot(_enemyClips[1]);
    }

    // Battle
    public void Alert()
    {
        _audio[(int) Audio.Battle].PlayOneShot(_battleClips[0]);
    }

    public void Damage()
    {
        _audio[(int) Audio.Battle].PlayOneShot(_battleClips[1]);
    }

    public void PhaseUp()
    {
        _audio[(int) Audio.Battle].PlayOneShot(_battleClips[2]);
    }

    public void Ult()
    {
        _audio[(int) Audio.Battle].PlayOneShot(_battleClips[3]);
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
        _isPlayed[i] = false;
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
        _audio[(int) Audio.UI].PlayOneShot(_uiClips[0]);
    }

    public void Focus()
    {
        _audio[(int) Audio.UI].PlayOneShot(_uiClips[1]);
    }

    public void Select()
    {
        _audio[(int) Audio.UI].PlayOneShot(_uiClips[2]);
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