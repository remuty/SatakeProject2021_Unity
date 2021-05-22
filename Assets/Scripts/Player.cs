using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider hpGauge;

    [SerializeField] private int maxHp;

    private int _hp;
    // Start is called before the first frame update
    void Start()
    {
        hpGauge.value = 1;
        _hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddDamage(int damage)
    {
        _hp -= damage;
        hpGauge.value = (float)_hp / (float)maxHp;
    }
}
