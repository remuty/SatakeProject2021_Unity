using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    
    [SerializeField] private Transform[] noteGeneratePositions = new Transform[2];
    
    [SerializeField] private Transform beatPosition;

    private AudioSource _audio;

    private float _generateTime;

    private float _time;
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
            generatedNote = Instantiate(notePrefab, transform.position, Quaternion.identity);
            generatedNote.GetComponent<Note>().SetTransform(noteGeneratePositions[1],beatPosition);
            _time = 0;
            _generateTime = 0.5f;
        }
    }
}
