using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolCard : MonoBehaviour
{
    public enum Element
    {
        Default,
        Fire,
        Ice,
        Lightning,
        Rock,
        Wind
    }
    [SerializeField] private GameObject symbolPrefab;
    [SerializeField] private Element element;
    [SerializeField] private GameObject attackEffect;
    
    private GameObject _symbolObject;
    public GameObject SymbolObject => _symbolObject;
    
    private Symbol _symbol;
    public Symbol Symbol => _symbol;

    private bool _isGenerated;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerateSymbolSlow()
    {
        if (!_isGenerated)
        {
            _isGenerated = true;
            Invoke("GenerateSymbol",0.6f);
        }
    }

    public void GenerateSymbol()
    {
        var canvas = GameObject.FindWithTag("FrontCanvas");
        _symbolObject = Instantiate(symbolPrefab,canvas.transform);
        _symbolObject.transform.SetSiblingIndex(1);
        _symbol = _symbolObject.GetComponent<Symbol>();
        _symbol.SetElement(element,attackEffect);
        _isGenerated = false;
    }
}