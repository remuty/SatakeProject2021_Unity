using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _notes;
    
    private AudioSource _audio;

    private float _generateTime;

    private float _time;

    private int _generatedCount;
    
    private float _checkRange = 1f;
    
    private float _beatRange = 0.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        _audio = this.GetComponent<AudioSource>();
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (_generatedCount < 6)
        {
            _time += Time.deltaTime;
            if (_time > _generateTime)
            {
                _notes[_generatedCount].SetActive(true);
                _notes[_generatedCount + 1].SetActive(true);
                _time = 0;
                _generateTime = 0.5f;
                _generatedCount += 2;
            }
        }
    }

    public bool CanBeat()
    {
        for (int i = 0; i < 6; i += 2 )
        {
            if (Mathf.Abs(_notes[i].transform.position.x) <= _checkRange)
            {
                _notes[i].GetComponent<SpriteRenderer>().enabled = false;
                _notes[i + 1].GetComponent<SpriteRenderer>().enabled = false;
                if (Mathf.Abs(_notes[i].transform.position.x) <= _beatRange)
                {
                    Debug.Log("成功:" + i + "pos:" + _notes[i].transform.position.x);
                    return true;
                }
                else
                {
                    Debug.Log("ミス:" + i + "pos:" + _notes[i].transform.position.x);
                    return false;
                }
            }
        }
        return false;
    }
}
