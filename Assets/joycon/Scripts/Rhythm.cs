using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm : MonoBehaviour
{
    public float bpm;
    public AudioClip se;
    AudioSource audioSource;
    public float beat_time;
    float default_value;
    public bool is_accept;
    public float accept_time;
    bool is_start = false;
    // Start is called before the first frame update
    void Start()
    {
        beat_time = 60 / bpm;
        audioSource = GetComponent<AudioSource>();
        default_value = beat_time;
        accept_time = beat_time / 4;
    }

    // Update is called once per frame
    void Update()
    {
        beat_time -= Time.deltaTime;
        
        if(beat_time <= accept_time / 2 && !is_start)
        {
            StartCoroutine("MakeSound");
            is_start = true;
        }
        
    }

    IEnumerator MakeSound()
    {
        is_accept = true;
        yield return new WaitForSeconds(accept_time / 2);
        audioSource.PlayOneShot(se);
        is_start = false;
        beat_time = default_value;
        yield return new WaitForSeconds(accept_time / 2);
        is_accept = false;
        yield return null;
    }

}
