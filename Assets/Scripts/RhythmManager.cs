using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform[] noteGeneratePositions = new Transform[2];
    [SerializeField] private Transform beatPosition;
    
    // [SerializeField] private GameObject[] _notes;
    private List<GameObject> _notes = new List<GameObject>();
    
    private AudioSource _audio;

    private float _generateTime;

    private float _time;

    private float _checkRange = 0.8f;
    
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
        _time += Time.deltaTime;
        if (_time > _generateTime)
        {
            var generatedNote = Instantiate(notePrefab, transform.position, Quaternion.identity);
            generatedNote.GetComponent<Note>().SetTransform(noteGeneratePositions[0],beatPosition);
            _notes.Add(generatedNote);
            generatedNote = Instantiate(notePrefab, transform.position, Quaternion.identity);
            generatedNote.GetComponent<Note>().SetTransform(noteGeneratePositions[1],beatPosition);
            _notes.Add(generatedNote);
            _time = 0;
            _generateTime = 0.5f;
        }
    }

    public bool CanBeat()
    {
        var ret = false;
        if (Mathf.Abs(_notes[0].transform.position.x) <= _checkRange)
        {
            if (Mathf.Abs(_notes[0].transform.position.x) <= _beatRange)
            {
                Debug.Log("成功:" + "pos:" + _notes[0].transform.position.x);
                ret = true;
            }
            else
            {
                Debug.Log("ミス:" + "pos:" + _notes[0].transform.position.x);
            }
            Destroy(_notes[0]);
            Destroy(_notes[1]);
            _notes.RemoveRange(0,2);
        }
        return ret;
    }

    public void RemoveNote()
    {
        _notes.RemoveAt(0);
    }
}
