using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletePopup : MonoBehaviour
{
    void Start()
    {
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void OnHomeButtonPressed(){
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestartButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }
}
