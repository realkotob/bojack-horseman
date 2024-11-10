using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
public class CardMatcher : MonoBehaviour
{

    private CardFlip currentCard = null;
    void Start()
    {
    }

    public void RegisterCard(CardFlip card)
    {
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
        }
        else
        {
            currentCard.SetNotMatched();
            card.SetNotMatched();
            currentCard = null;
        }
    }
}
}