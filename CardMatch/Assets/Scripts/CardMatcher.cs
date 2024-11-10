using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
public class CardMatcher : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    private ScoreManager scoreManager;

    private CardFlip currentCard = null;

    private List<CardFlip> cardsList = new List<CardFlip>();

    void Start()
    {
    }

    public void RegisterCard(CardFlip card)
    {
        cardsList.Add(card);
        card.cardOpened += OnCardOpened;
    }

    public void OnCardOpened(CardFlip card)
    {
        if (currentCard == null)
        {
            currentCard = card;
            return;
        }

        if (currentCard == card)
        {
            return;
        }

        if (currentCard.GetCardId() == card.GetCardId())
        {
            currentCard.SetMatched();
            card.SetMatched();
            currentCard = null;

            scoreManager.IncreaseScoreAndCombo();

            CheckGameComplete();
        }
        else
        {
            currentCard.SetNotMatched();
            card.SetNotMatched();
            currentCard = null;

            scoreManager.ResetCombo();
        }
    }

    public void CheckGameComplete()
    {
        var gameComplete = true;
        foreach (var item in cardsList)
        {
            if (!item.GetIsMatched())
            {
                gameComplete = false;
            }
        }

        if (gameComplete)
        {
            // Save empty file when game is finished
            SaveLoadManager.SaveData(new CardData[0]);

            // TODO Show win popup
        }
        else
        {
            SaveLoadManager.SaveData(GetDataArray());
        }
    }

    public CardData[] GetDataArray()
    {
        var cardDataList = new List<CardData>();
        foreach (var item in cardsList)
        {
            cardDataList.Add(item.GetCardData());
        }
        return cardDataList.ToArray();
    }
}
}