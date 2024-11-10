using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CardMatch
{
public class MenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Button continueButton;

    void Start()
    {
        var saveData = SaveLoadManager.LoadData();
        if (saveData.Length > 0)
        {
            continueButton.interactable = true;
        }
    }

    public void OnContinuePressed()
    {
        PlayerPrefs.SetInt("ContinueGame", 1);

        SceneManager.LoadScene("GameScene");
    }

    public void OnNewGamePressed(int cardCount){
        PlayerPrefs.SetInt("ContinueGame", 0);
        PlayerPrefs.SetInt("CardCount", cardCount);
        
        SceneManager.LoadScene("GameScene");
    }
}
}