using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    void Start()
    {
        SpawnCards();
    }

    private void SpawnCards()
    {
        // Remove all existing cards
        foreach (Transform child in gridLayout.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < cardCount; i++)
        {
            Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, gridLayout.transform);
        }
    }
}
