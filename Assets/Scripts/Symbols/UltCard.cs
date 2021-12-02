using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltCard : MonoBehaviour
{
    [SerializeField] private Image ultGauge;
    [SerializeField]private int _countMax;
    
    private SoundManager _sound;
    private float _count;
    private bool _isMaxed;
    
    // Start is called before the first frame update
    void Start()
    {
        _sound = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void AddCount(int combo)
    {
        if (_count < _countMax)
        {
            _count += combo;
            if (_count >= _countMax)
            {
                _count = _countMax;
                _isMaxed = true;
                _sound.Ult();
            }
            ultGauge.fillAmount = _count / _countMax;
        }
    }
}
