using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform[] noteGeneratePositions = new Transform[2];
    [SerializeField] private Transform beatPosition;
    [SerializeField] private Text comboText;
    [SerializeField] private Text comboSubText;
    
    // [SerializeField] private GameObject[] _notes;
    private List<GameObject> _notes = new List<GameObject>();
    
    private AudioSource _audio;

    private float _generateTime;

    private float _time = -0.6f;

    [SerializeField] private float _checkRange = 0.8f;
    
    [SerializeField] private float _beatRange = 0.4f;
    private int _combo;
    public int Combo => _combo;
    public enum Beat
    {
        noReaction,
        good,
        miss
    }

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
            var generatedNote = Instantiate(notePrefab,
                transform.position, Quaternion.identity, this.transform);
            generatedNote.GetComponent<Note>().SetTransform(noteGeneratePositions[0],beatPosition);
            _notes.Add(generatedNote);
            generatedNote = Instantiate(notePrefab, transform.position, Quaternion.identity, this.transform);
            generatedNote.GetComponent<Note>().SetTransform(noteGeneratePositions[1],beatPosition);
            _notes.Add(generatedNote);
            _time = 0;
            _generateTime = 0.5f;
        }

        if (_combo >= 2)
        {
            comboText.text = $"{_combo}";
            comboSubText.text = "連";
        }
        else
        {
            comboText.text = "";
            comboSubText.text = "";
        }
    }

    public Beat CanBeat()
    {
        var ret = Beat.noReaction;
        if (Mathf.Abs(_notes[0].transform.position.x) <= _checkRange)
        {
            if (Mathf.Abs(_notes[0].transform.position.x) <= _beatRange)
            {
                // Debug.Log("成功:" + "pos:" + _notes[0].transform.position.x);
                _combo++;
                ret = Beat.good;
            }
            else
            {
                // Debug.Log("ミス:" + "pos:" + _notes[0].transform.position.x);
                _combo = 0;
                ret = Beat.miss;
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
