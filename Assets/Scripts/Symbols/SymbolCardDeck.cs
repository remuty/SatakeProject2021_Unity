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
        int[] cardIds = {0, 1, 2, 3, 4, 5, 6, 3, 0, 1};
        CreateCardDeck(cardIds);
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectedCard.Symbol != null && !_selectedCard.Symbol.activeSelf)
        {
            _selectedCard.Symbol.SetActive(true);
        }
    }

    void CreateCardDeck(int[] cardIds)
    {
        _deck = new GameObject[10];
        for (int i = 0; i < _deck.Length; i++)
        {
            var id = cardIds[i];
            if (i < cardPositions.Length)
            {
                _deck[i] = Instantiate(symbolCardPrefabs[id], cardPositions[i], Quaternion.identity);
            }
            else
            {
                _deck[i] = Instantiate(symbolCardPrefabs[id], deckPosition, Quaternion.identity);
            }
        }

        _selectedCard = _deck[0].GetComponent<SymbolCard>();
    }

    void DrawCard()
    {
    }
}