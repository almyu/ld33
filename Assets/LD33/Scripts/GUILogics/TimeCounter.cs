using UnityEngine;
using System;

public class TimeCounter : MonoSingleton<TimeCounter> {
    private float _timeElapsed;
	
	// Update is called once per frame
	void Update () {
        _timeElapsed += Time.deltaTime;
    }

    public string GetElapsedTime() {
        var minutes = Mathf.Floor(_timeElapsed / 60);
        var seconds = (_timeElapsed % 60);

        if (seconds == 60) {
            seconds = 0;
            minutes += 1;
        }

        return String.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
