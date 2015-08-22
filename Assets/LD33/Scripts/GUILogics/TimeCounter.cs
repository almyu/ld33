﻿using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class TimeCounter : MonoSingleton<TimeCounter> {
    public float TimeLeft;
    public UnityEvent TimeOver;
    private Text _timerControl;
	// Use this for initialization
	void Start () {
        _timerControl = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        TimeLeft -= Time.deltaTime;

        var minutes = Mathf.Floor(TimeLeft / 60).ToString("00");
        var seconds = (TimeLeft % 60).ToString("00"); ;

        if(seconds == "60") {
            return;
        }
        _timerControl.text = String.Format("Time left: {0}:{1}", minutes, seconds);

        if(TimeLeft <= 0) {
            if (TimeOver == null) {
                Debug.LogWarning("TimeOver event is null!");
                return;
            }

            TimeOver.Invoke();
        }
    }
}
