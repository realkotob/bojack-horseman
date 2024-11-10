using System;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch
{
public class CardFlip : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField]
    private float flipSpeed = 0.5f;

    [SerializeField]
    private float matchDisappearTime = 2f;

    [SerializeField]
    private float unmatchWaitTime = 2.5f;

    [Header("References")]
    [SerializeField]
    private Transform rotationParent;
    [SerializeField]
    private Image imageBack;
    [SerializeField]
    private Image imageFront;

    [SerializeField]
    private RandomSFX flipSound;

#region Public variables

    internal Action<CardFlip> cardOpened;

#endregion

#region Private variables

    private Quaternion frontRotation;
    private Quaternion backRotation;

    private bool isOpen = false;
    private bool isFlipping = false;

    private int cardId = 0;

    private bool isMatched = false;

#endregion

    void Start()
    {
        backRotation = rotationParent.rotation;
        frontRotation = Quaternion.Euler(rotationParent.eulerAngles + new Vector3(0, 180, 0));
        imageFront.enabled = false;
    }

    public void Initialize(int cardId, Sprite sprite, bool cardInPlay)
    {
        imageFront.sprite = sprite;
        this.cardId = cardId;

        if (!cardInPlay)
        {
            isMatched = true;
            HideCard();
        }
    }

#region Flip helpers

    void Update()
    {
        if (!imageBack.gameObject.activeInHierarchy)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (isMatched || isOpen)
            {
                return;
            }
            if (Input.touchCount > 0)
            {
                CheckPressed(Input.GetTouch(0).position);
            }
            else
            {
                CheckPressed(Input.mousePosition);
            }
        }

        ExecuteFlipping();
    }

    private void CheckPressed(Vector2 inputPosition)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), inputPosition))
        {
            FlipCard();
        }
    }

    private void FlipCard()
    {
        isOpen = !isOpen;
        isFlipping = true;
        // Debug.Log("Flip card to " + isOpen);

        flipSound.Play();

        if (isOpen)
        {
            cardOpened?.Invoke(this);
        }
    }

    private void ExecuteFlipping()
    {
        if (isFlipping)
        {
            float step = flipSpeed * Time.deltaTime;
            Quaternion targetRotation;
            if (isOpen)
            {
                targetRotation = frontRotation;
            }
            else
            {
                targetRotation = backRotation;
            }
            
            rotationParent.rotation = Quaternion.RotateTowards(rotationParent.rotation, targetRotation, step * 180);
            var angle = Quaternion.Angle(rotationParent.rotation, targetRotation);
            if (angle < 1f)
            {
                rotationParent.rotation = targetRotation;
                isFlipping = false;
            }
            if (angle < 90)
            {
                imageFront.enabled = isOpen;
                imageBack.enabled = !isOpen;
            }
        }
    }

#endregion

#region Matching helpers

    public void SetMatched()
    {
        isMatched = true;

        Invoke(nameof(HideCard), matchDisappearTime);
    }

    private void HideCard()
    {
        imageBack.gameObject.SetActive(false);
    }

    public void SetNotMatched()
    {
        Invoke(nameof(CloseCard), unmatchWaitTime);
    }

    private void CloseCard()
    {
        isOpen = false;
        isFlipping = true;
    }

    public int GetCardId()
    {
        return cardId;
    }

    public bool GetIsMatched()
    {
        return isMatched;
    }

    public CardData GetCardData()
    {
        return new CardData(cardId, !isMatched);
    }

#endregion
}
}