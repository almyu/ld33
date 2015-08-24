using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoSingleton<ScoreCounter> {
    private int _currentScore;
    private Text _scoreControl;
    private int _booCounter = 0;

    void Start() {
        _scoreControl = GetComponent<Text>();
        Add(0);
    }

    public void Add(int valueToAdd) {
        _currentScore += valueToAdd;
        _scoreControl.text = String.Format("Score: {0:0000000}", _currentScore);
    }

    public int GetScore() {
        return _currentScore;
    }

    private void Update() {
        ScoreTester();
    }

    private void ScoreTester() {
        if (Input.GetKeyDown(key: KeyCode.Keypad9)) {
            Add(300);
        }
    }

    public int GetFinalScore() {
        var factor = _booCounter != 0 ? _booCounter : 1;
        return factor * _currentScore;
    }

    public void BooPointFound() {
        _booCounter += 1;
        if (_booCounter == TriggerBar.instance.triggers.Length) {
            GameOverController.instance.ShowGameOver(win: true);
        }
    }
}
