﻿using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoSingleton<GameOverController> {
    private GameObject _gameOverText;
    private GameObject _restartButton;
    private Text _restartText;
    private bool _gameOver = false;
    private int _booCounter = 0;
    private void Awake() {
        _gameOverText = GameObject.FindGameObjectWithTag("GameOverText");
        _restartButton = GameObject.FindGameObjectWithTag("RestartButton");
        _restartText = _restartButton.GetComponentInChildren<Text>();
    }

    public void ShowGameOver(bool win) {
        if (_gameOver)
            return;
        _gameOver = true;

        _restartButton.GetComponent<Image>().enabled = true;
        _restartButton.GetComponent<Button>().enabled = true;
        _restartText.enabled = true;
        _restartButton.GetComponent<Button>().onClick.AddListener(Restart);

        var factor = _booCounter != 0 ? _booCounter : 1;
        var finalScore = factor * ScoreCounter.instance.GetScore();

        var text = string.Empty;
        text += win ? "You win!" : "You've lost!";
        text += "\n";
        text += TimeCounter.instance.GetElapsedTime();
        text += "\n";
        text += "Score: " + finalScore;

        _gameOverText.GetComponent<Text>().text = text;
    }

    private void Restart() {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void BooPointFound() {
        _booCounter += 1;
        if (_booCounter == Balance.instance.BooPointsCount) {
            ShowGameOver(win: true);
        }
    }

    public int FoundBooPoints() {
        return _booCounter;
    }
}
