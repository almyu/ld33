using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class TimeCounter : MonoSingleton<TimeCounter> {
    private float _timeLeft;
    public UnityEvent TimeOver;
    private Text _timerControl;
	// Use this for initialization
	void Start () {
        _timerControl = GetComponent<Text>();
        _timeLeft = Balance.instance.LevelDuration;
    }
	
	// Update is called once per frame
	void Update () {
        _timeLeft -= Time.deltaTime;

        var minutes = Mathf.Floor(_timeLeft / 60).ToString("00");
        var seconds = (_timeLeft % 60).ToString("00");

        if (_timeLeft <= 0) {
            _timerControl.text = "Time left: 00:00";
            GameOverController.instance.ShowGameOver(win: true);

            if (TimeOver != null) {
                TimeOver.Invoke();
            }

            enabled = false;
            return;
        }

        if (seconds == "60") {
            return;
        }
        _timerControl.text = String.Format("Time left: {0}:{1}", minutes, seconds);


    }
}
