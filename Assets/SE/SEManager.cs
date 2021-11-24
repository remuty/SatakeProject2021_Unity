using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    AudioSource[] sounds;

    // Use this for initialization
    void Start()
    {
        sounds = GetComponents<AudioSource>();
    }

    // Battle Screen
    // Attack
    void SE(int num)
    {
        sounds[num].PlayOneShot(sounds[num].clip);
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
        SE(10);
    }

    public void LevelUp()
    {
        SE(11);
    }

    public void GettingCard()
    {
        SE(17);
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