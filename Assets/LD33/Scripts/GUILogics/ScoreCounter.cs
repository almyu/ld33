using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoSingleton<ScoreCounter> {
    private int _currentScore;
    private Text _scoreControl;

    void Start() {
        _scoreControl = GetComponent<Text>();
        Add(0);
    }

    public void Add(int valueToAdd) {
        _currentScore += valueToAdd;
        _scoreControl.text = String.Format("Score: {0:0000000}", _currentScore);
    }

    public string GetScore() {
        return _currentScore.ToString("0000000");
    }

    private void Update() {
        ScoreTester();
    }

    private void ScoreTester() {
        if (Input.GetKeyDown(key: KeyCode.Keypad9)) {
            Add(300);
        }
    }
}
