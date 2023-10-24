using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] GameObject _winText;
    [SerializeField] GameObject _loseText;
    [SerializeField] TMP_Text _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnWin += ShowWin;
        GameManager.Instance.OnLose += ShowLose;
    }

    void OnDisable()
    {
        GameManager.Instance.OnWin -= ShowWin;
        GameManager.Instance.OnLose -= ShowLose;
    }

    void ShowWin()
    {
        _winText.SetActive(true);
        ShowScreen();
    }

    void ShowLose()
    {
        _loseText.SetActive(true);
        ShowScreen();
    }

    void ShowScreen()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        int currentScore = PlayerCandyLauncher.Instance.CurrentAmmo;
        if (highScore < currentScore)
            highScore = currentScore;
        PlayerPrefs.SetInt("HighScore", highScore);
        _scoreText.SetText($"High Score: {highScore}");
        _gameOverScreen.SetActive(true);
    }
}
