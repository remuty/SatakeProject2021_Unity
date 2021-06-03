using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolCardDeck : MonoBehaviour
{
    [SerializeField] private Vector2[] cardPositions;
    [SerializeField] private Vector2 deckPosition;

    [SerializeField] private GameObject[] symbolCardPrefabs;

    private GameObject[] _deck;

    [SerializeField] private GameObject symbol;
    [SerializeField] private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        int[] cardIds = {0, 1, 2, 3, 0, 1, 2, 3, 0, 1};
        CreateCardDeck(cardIds);
        var prefab = Instantiate(symbol);
        prefab.transform.SetParent(canvas.transform,false);
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void DrawCard()
    {
    }
}