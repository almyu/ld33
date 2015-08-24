using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AlertCounter : MonoSingleton<AlertCounter> {

    public AnimationCurve alertCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
    public UnityEvent alertFired;

    private Slider _slider;
    private float _timeElapsedBetweenAlarmFires;
    private bool _gameOver = false;
    private float _timeSinceLevelLoad = 2.0f;
    private float _elapsed = 0.0f;
    private float _doorOpenerTimer;
    // Use this for initialization
    private void Awake () {
        _slider = GetComponent<Slider>();
        _timeElapsedBetweenAlarmFires = Balance.instance.TimeBetweenAlarms;
        _doorOpenerTimer = Balance.instance.DoorOpenerTimer;
    }
    
    public void Add(float valueToAdd) {
        if (_elapsed < _timeSinceLevelLoad)
            return;
        
        valueToAdd *= alertCurve.Evaluate(_slider.normalizedValue);

        var newValue = _slider.value + valueToAdd;
        _slider.value = newValue;

        if (newValue >= _slider.maxValue) {
            GameOverController.instance.ShowGameOver(win: false);
            _gameOver = true;
            enabled = false;
            return;
        }

        if (_timeElapsedBetweenAlarmFires <= Balance.instance.TimeBetweenAlarms) {
            return;
        }
        
        if (newValue >= Balance.instance.AlarmLevelFlashight && newValue < Balance.instance.AlarmLevelDoorLight) {
            FlashlightController.instance.FollowPlayer(true);
            _timeElapsedBetweenAlarmFires = 0.0f;
        }

        alertFired.Invoke();
    }
	
	// Update is called once per frame
    private void Update () {
        if (_gameOver)
            return;

        _elapsed += Time.deltaTime;

        _doorOpenerTimer -= Time.deltaTime;
        if (_doorOpenerTimer <= 0) {
            DoorController.instance.enabled = true;
            _doorOpenerTimer = Balance.instance.DoorOpenerTimer;
        }

        _slider.value -= (Time.deltaTime * Balance.instance.AlarmDecreasingFactor);

        if (_slider.value < Balance.instance.AlarmLevelFlashight) {
            FlashlightController.instance.FollowPlayer(false);
        }

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
