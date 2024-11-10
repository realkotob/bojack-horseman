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

    [SerializeField]
    private AudioManager audioManager;

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

            audioManager.Invoke(nameof(AudioManager.PlayMatchSound), 0.2f);

            CheckGameComplete();
        }
        else
        {
            currentCard.SetNotMatched();
            card.SetNotMatched();
            currentCard = null;

            audioManager.Invoke(nameof(AudioManager.PlayMismatchSound), 0.2f);

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

            Invoke(nameof(ShowLevelCompletePopup), 0.5f);
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

    public void ShowLevelCompletePopup()
    {
        audioManager.PlayLevelComplete();

        // TODO Show win popup
    }
}
}