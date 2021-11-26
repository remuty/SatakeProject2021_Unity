using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform[] noteGeneratePositions = new Transform[2];
    [SerializeField] private Transform beatPosition;
    [SerializeField] private Text comboText;
    [SerializeField] private Text comboSubText;
    [SerializeField] private Text waveText;
    [SerializeField] private double[] bpm;
    //BGM
    [SerializeField] private AudioClip[] bgm; 
    //Wave切り替え時カットイン画像(Canvasの子オブジェクトに用意してあるものが割り当てられる想定)
    [SerializeField] private Image waveSwitchImage;
    //団子飛んでくるときのノーツスプライト
    [SerializeField] private Sprite dangoSprite;


    // [SerializeField] private GameObject[] _notes;
    private List<GameObject> _notes = new List<GameObject>();
    
    private AudioSource _audio;
    
    private double _metronomeStartDspTime;

    private double _interval;

    private double _buffer;

    private double _time;

    private double _oldTime;
    
    [SerializeField] private float _checkRange = 0.8f;
    
    [SerializeField] private float _beatRange = 0.4f;

    private int _combo;
    public int Combo => _combo;

    //Wave数
    private int _wave;
    public int Wave => _wave;
    //Wave切り替え中かどうか
    private bool _isSwitching;
    public bool IsSwitching => _isSwitching;
    //次のノーツを団子にするかどうか（デバッグ用にSelializeField）
    [SerializeField]
    private bool _isWarning;
    public bool IsWarning
    {
        set => _isWarning = value;
    }

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
        _audio.clip = bgm[0];
        _audio.Play();
        _interval = 1d / (bpm[0] / 60d);
        _metronomeStartDspTime = AudioSettings.dspTime;
        _wave = 1;
        //音の再生が止まったらWave切り替えスタート
        this.UpdateAsObservable()
            .Where(_ => !_audio.isPlaying)
            .ThrottleFirst(System.TimeSpan.FromSeconds(10f))
            .Subscribe(_ => StartCoroutine("SwitchWave"));
    }

    // Update is called once per frame
    void Update()
    {
        var elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        _time = elapsedDspTime - _oldTime;
        if (_time > _interval)
        {
            //Wave切り替え中はNoteを生成しない
            if (!_isSwitching)
            {
                GameObject.FindWithTag("Target").GetComponent<NormalEnemy>().IsAttacking = true;
                var generatedNote = Instantiate(notePrefab,
                transform.position, Quaternion.identity, this.transform);
                generatedNote.GetComponent<Note>().SetTransform(noteGeneratePositions[0], beatPosition);
               
                _notes.Add(generatedNote);
                generatedNote = Instantiate(notePrefab, transform.position, Quaternion.identity, this.transform);
                generatedNote.GetComponent<Note>().SetTransform(noteGeneratePositions[1], beatPosition);
                
                _notes.Add(generatedNote);
            }
            _buffer = _time - _interval;
            _oldTime += _time - _buffer;
        }
        
        if (_notes.Count > 0)
        {
            if (_isWarning) {    //泥団子に切り替える
                _notes[_notes.Count - 1].GetComponent<SpriteRenderer>().sprite = dangoSprite;
                _notes[_notes.Count - 2].GetComponent<SpriteRenderer>().sprite = dangoSprite;
                _isWarning = false;
            }
            if (Mathf.Abs(_notes[0].transform.position.x) <= 0.001f)
            {
                _combo = 0;
            }
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

    public void NotesCheck()    //ノーツを見逃したらコンボリセット
    {
        if (_notes.Count > 0)
        {
            if (Mathf.Abs(_notes[0].transform.position.x) <= 0.001f)
            {
                _combo = 0;
            }
        }
    }
    
    public void RemoveNote()
    {
        _notes.RemoveAt(0);
    }
    
    IEnumerator SwitchWave()
    {
        _wave++;
        waveSwitchImage.enabled = true;
        waveText.text = $"{_wave}";
        waveText.enabled = true;
        _isSwitching = true;
        yield return new WaitForSeconds(5f);
        waveText.enabled = false;
        waveSwitchImage.enabled = false;
        _isSwitching = false;
        _audio.clip = bgm[(_wave - 1) % bgm.Length];
        _interval = 1d / (bpm[(_wave - 1) % bgm.Length] / 60d);
        _audio.Play();
    }
}
