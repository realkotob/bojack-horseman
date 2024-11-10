using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch
{
public class GridResizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GridLayoutGroup gridLayoutGroup;
    [SerializeField]
    private RectTransform gridRectTransform;

    void Start()
    {
    }

    public void ResizeCardsToFitGrid()
    {
        if (gridLayoutGroup == null || gridRectTransform == null)
        {
            Debug.LogError("gridLayoutGroup or gridRectTransform is null");
            return;
        }

        var numCards = gridRectTransform.childCount;
        if (numCards <= 0)
        {
            return;
        }

        float gridWidth = gridRectTransform.rect.width;
        float gridHeight = gridRectTransform.rect.height;
        var aspectRatio = gridLayoutGroup.cellSize.x / gridLayoutGroup.cellSize.y;

        // Initialize optimal variables
        float bestCellSize = 0f;
        int bestColumns = 1;
        int bestRows = numCards;

        // Find best fit column and row setup
        for (int columns = 1; columns <= numCards; columns++)
        {
            int rows = Mathf.CeilToInt((float)numCards / columns);

            // Calculate max cell width and height for this setup
            float maxCellWidth = (gridWidth - (gridLayoutGroup.spacing.x * (columns - 1))) / columns;
            float maxCellHeight = (gridHeight - (gridLayoutGroup.spacing.y * (rows - 1))) / rows;

            // Adjust cell size based on card aspect ratio
            float cellWidth = Mathf.Min(maxCellWidth, maxCellHeight * aspectRatio);
            float cellHeight = cellWidth / aspectRatio;

            // Ensure the calculated cell size fits in the grid
            if (cellHeight <= maxCellHeight && cellWidth <= maxCellWidth)
            {
                float cellSize = Mathf.Min(cellWidth, cellHeight);

                // Track optimal settings
                if (cellSize > bestCellSize && (columns % 2 == 0))
                {
                    bestCellSize = cellSize;
                    bestColumns = columns;
                    bestRows = rows;
                }
            }
        }

        // Use new settings
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = bestColumns;
        // var scaleChange = bestCellSize/ gridLayoutGroup.cellSize.x;
        gridLayoutGroup.cellSize = new Vector2(bestCellSize, bestCellSize / aspectRatio);

        foreach (Transform child in gridLayoutGroup.transform)
        {
            RectTransform[] cardRectTransforms = child.GetComponentsInChildren<RectTransform>();
            foreach (var item in cardRectTransforms)
            {
                item.sizeDelta = gridLayoutGroup.cellSize;
            }
        }
    }
}
}