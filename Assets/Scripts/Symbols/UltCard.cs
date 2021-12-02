using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltCard : MonoBehaviour
{
    [SerializeField] private Image ultGauge;

    [SerializeField]private int _countMax;
    private float _count;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_count >= _countMax)
        {
            _count = _countMax;
        }
        ultGauge.fillAmount = _count / _countMax;
    }

    public void AddCount(int combo)
    {
        _count += combo;
    }
}
