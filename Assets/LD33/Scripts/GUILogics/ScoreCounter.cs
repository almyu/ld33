using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoSingleton<ScoreCounter> {

    public int booScore = 200;

    private int _currentScore;
    private Text _scoreControl;
    private int _booCounter = 0;
    private float _timeSinceLevelLoad = 2.0f;
    private float _elapsed = 0.0f;

    void Start() {
        _scoreControl = GetComponent<Text>();
        Add(0);
    }

    public void Add(int valueToAdd) {
        //because at start some objects fall and make noise
        if (_elapsed < _timeSinceLevelLoad)
            return;

        _currentScore += valueToAdd;
        _scoreControl.text = String.Format("{0:0000000}", _currentScore);
    }

    public int GetScore() {
        return _currentScore;
    }

    private void Update() {
        _elapsed += Time.deltaTime;
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
        Add(booScore);

        _booCounter += 1;
        if (_booCounter == TriggerBar.instance.triggers.Length) {
            GameOverController.instance.ShowGameOver(win: true);
        }
    }
}
