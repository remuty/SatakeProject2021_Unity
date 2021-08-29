using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolCard : MonoBehaviour
{
    [SerializeField] private GameObject symbolPrefab;
    private GameObject _canvas;
    
    private GameObject _symbolObject;
    public GameObject SymbolObject => _symbolObject;
    
    private Symbol _symbol;
    public Symbol Symbol => _symbol;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GameObject.FindWithTag("FrontCanvas");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerateSymbol()
    {
        _symbolObject = Instantiate(symbolPrefab,_canvas.transform);
        _symbol = _symbolObject.GetComponent<Symbol>();
    }
}