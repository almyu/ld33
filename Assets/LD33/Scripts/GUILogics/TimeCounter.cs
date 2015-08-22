using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeCounter : MonoBehaviour {

    public float TimeLeft;
    private Text _timerControl;
	// Use this for initialization
	void Start () {
        _timerControl = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        TimeLeft -= Time.deltaTime;

        var minutes = TimeLeft / 60;
        var seconds = TimeLeft % 60;

        _timerControl.text = String.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
