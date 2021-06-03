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
    // Start is called before the first frame update
    void Start()
    {
        CreateCardDeck();
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectedCard.Symbol != null && !_selectedCard.Symbol.activeSelf)
        {
            _selectedCard.Symbol.SetActive(true);
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

    void DrawCard()
    {
    }
}