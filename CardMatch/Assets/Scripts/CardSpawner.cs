using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch
{
public class CardSpawner : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField]
    private int cardCount = 30;

    [Header("References")]
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private GameObject gridLayout;

    [SerializeField]
    private GridResizer gridResizer;

    [SerializeField]
    private CardMatcher cardMatcher;

    [SerializeField]
    private List<Sprite> heartsSprites = new List<Sprite>();

    [SerializeField]
    private List<Sprite> diamondsSprites = new List<Sprite>();

    [SerializeField]
    private List<Sprite> clubsSprites = new List<Sprite>();

    [SerializeField]
    private List<Sprite> spadesSprites = new List<Sprite>();

#region Private Variables

    private List<Sprite> allSprites = new List<Sprite>();

    private List<int> allCardIds = new List<int>();

    private System.Random random = new System.Random();

    private List<CardFlip> cardsList = new List<CardFlip>();

#endregion

    void Start()
    {
        ResetBoard();

        CreateCardDeck();

        if (PlayerPrefs.GetInt("ContinueGame", 0) == 1)
        {
            var saveData = SaveLoadManager.LoadData();
            if (saveData.Length > 0)
            {
                SpawnCards(saveData);
                return;
            }
        }

        cardCount = PlayerPrefs.GetInt("CardCount", 12);

        var cardData = SelectRandomCards(cardCount);
        SpawnCards(cardData);
    }

    private void ResetBoard()
    {
        // Remove all existing cards
        foreach (Transform child in gridLayout.transform)
        {
            Destroy(child.gameObject);
        }
        cardsList = new List<CardFlip>();
    }

    private void CreateCardDeck()
    {
        allSprites = new List<Sprite>();
        allSprites.AddRange(heartsSprites);
        allSprites.AddRange(diamondsSprites);
        allSprites.AddRange(clubsSprites);
        allSprites.AddRange(spadesSprites);

        allCardIds = new List<int>();
        for (int i = 0; i < allSprites.Count; i++)
        {
            allCardIds.Add(i);
        }
    }

    private void SpawnCards(CardData[] saveData)
    {
        for (int i = 0; i < saveData.Length; i++)
        {
            var cardObject = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, gridLayout.transform);
            var cardFlipComponent = cardObject.GetComponent<CardFlip>();

            var cardId = saveData[i].cardId;
            cardFlipComponent.Initialize(cardId, allSprites[cardId], saveData[i].cardInPlay);

            cardMatcher.RegisterCard(cardFlipComponent);
            cardsList.Add(cardFlipComponent);
        }

        gridResizer.ResizeCardsToFitGrid();
    }

    private CardData[] SelectRandomCards(int totalCards)
    {
        Shuffle(allCardIds);

        var cardIds = allCardIds.Take(totalCards / 2).ToList();

        cardIds.AddRange(cardIds); // So that the cards are in pairs

        Shuffle(cardIds);

        CardData[] cardData = new CardData[cardIds.Count];
        for (int i = 0; i < cardData.Length; i++)
        {
            cardData[i] = new CardData(cardIds[i], true);
        }

        return cardData;
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
}