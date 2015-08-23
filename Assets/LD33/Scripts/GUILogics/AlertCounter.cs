using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AlertCounter : MonoSingleton<AlertCounter> {
    public UnityEvent alertFired;

    private Slider _slider;
    private float _timeElapsedBetweenAlarmFires;
    private bool _gameOver = false;
    // Use this for initialization
    private void Awake () {
        _slider = GetComponent<Slider>();
        _timeElapsedBetweenAlarmFires = Balance.instance.TimeBetweenAlarms;
    }
    
    public void Add(float valueToAdd) {
        var newValue = _slider.value + valueToAdd;
        _slider.value = newValue;

        if (newValue >= _slider.maxValue) {
            GameOverController.instance.ShowGameOver(win: false);
            _gameOver = true;
        }

        if (_timeElapsedBetweenAlarmFires <= Balance.instance.TimeBetweenAlarms) {
            return;
        }

        if (newValue >= Balance.instance.AlarmLevelFlashight && newValue < Balance.instance.AlarmLevelDoorLight) {
            FlashlightController.instance.enabled = true;
            _timeElapsedBetweenAlarmFires = 0.0f;
        }

        if (newValue >= Balance.instance.AlarmLevelDoorLight && newValue < Balance.instance.AlarmLevelTopLight) {
            DoorController.instance.enabled = true;
            _timeElapsedBetweenAlarmFires = 0.0f;
        }
        
        if (newValue >= Balance.instance.AlarmLevelTopLight) {
            //TODO: Turn on the light
            //_timeElapsedBetweenAlarmFires = 0.0f;
        }

        if (alertFired != null) {
            alertFired.Invoke();
        }
    }
	
	// Update is called once per frame
    private void Update () {
        if (_gameOver)
            return;

        _slider.value -= (Time.deltaTime * Balance.instance.AlarmDecreasingFactor);

        _timeElapsedBetweenAlarmFires += Time.deltaTime;

        //TODO: Remove before release!!!
        AlarmTester();
        DoorTester();
        FlashlightTester();
    }

    private void AlarmTester() {
        if (Input.GetKeyDown(key: KeyCode.KeypadPlus)) {
            Add(30);
        }
    }

    private void DoorTester() {
        if (Input.GetKeyDown(key: KeyCode.KeypadDivide)) {
            DoorController.instance.enabled = true;
        }
    }

    private void FlashlightTester() {
        if (Input.GetKeyDown(key: KeyCode.KeypadMultiply)) {
            FlashlightController.instance.enabled = true;
        }
    }
}
