using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI comboText;

    private int currentScore = 0;
    private int currentCombo = 1;

    void Start()
    {
        UpdateUIText();
    }

    public void IncreaseScoreAndCombo()
    {
        currentScore += currentCombo;

        currentCombo += 1;

        UpdateUIText();
    }

    public void ResetCombo()
    {
        currentCombo = 1;

        UpdateUIText();
    }

    private void UpdateUIText(){
        scoreText.text = "Score: " + currentScore.ToString();
        comboText.text = "Combo: " + currentCombo.ToString() + "x";
    }
}
