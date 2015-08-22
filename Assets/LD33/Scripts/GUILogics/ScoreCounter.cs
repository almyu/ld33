﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {
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
}