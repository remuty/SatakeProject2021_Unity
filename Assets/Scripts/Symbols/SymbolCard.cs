using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolCard : MonoBehaviour
{
    [SerializeField] private GameObject symbolPrefab;
    private GameObject _canvas;
    
    private GameObject _symbol;
    public GameObject Symbol => _symbol;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GameObject.FindWithTag("FrontCanvas");
        _symbol = Instantiate(symbolPrefab);
        _symbol.SetActive(false);
        _symbol.transform.SetParent(_canvas.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}