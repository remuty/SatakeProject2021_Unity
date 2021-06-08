using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolCardDeck : MonoBehaviour
{
    [SerializeField] private Vector2[] cardPositions;
    [SerializeField] private Vector2 deckPosition;

    [SerializeField] private GameObject[] symbolCardPrefabs;

    private GameObject[] _deck;

    private SymbolCard _selectedCard;

    public SymbolCard SelectedCard => _selectedCard;

    private int _drawNum = 4;
    // Start is called before the first frame update
    void Start()
    {
        CreateCardDeck();
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectedCard.SymbolObject != null && !_selectedCard.SymbolObject.activeSelf)
        {
            _selectedCard.SymbolObject.SetActive(true);
        }
    }

    void CreateCardDeck()
    {
        _deck = new GameObject[symbolCardPrefabs.Length];
        List<int> num = new List<int>();
        for (int i = 0; i < _deck.Length; i++)
        {
            num.Add(i);
        }
        for (int i = 0; i < _deck.Length; i++)
        {
            var id = num[Random.Range(0,num.Count)];
            if (i < cardPositions.Length)
            {
                _deck[i] = Instantiate(symbolCardPrefabs[id], cardPositions[i], Quaternion.identity);
            }
            else
            {
                _deck[i] = Instantiate(symbolCardPrefabs[id], deckPosition, Quaternion.identity);
            }
            num.Remove(id);
        }

        _selectedCard = _deck[0].GetComponent<SymbolCard>();
    }

    public void DrawCard() //TODO:デッキからドローする処理
    {
        _selectedCard.SymbolObject.SetActive(false);
        var card = _deck[0];
        _deck[0] = _deck[_drawNum];
        _deck[_drawNum] = card;
        _deck[_drawNum].transform.position = deckPosition;
        _deck[0].transform.position = cardPositions[0];
        _selectedCard = _deck[0].GetComponent<SymbolCard>();
        _drawNum++;
        if (_drawNum >= _deck.Length)
        {
            _drawNum = 4;
        }
    }
}