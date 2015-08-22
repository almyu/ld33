using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AlertCounter : MonoSingleton<AlertCounter> {
    private Slider _slider;
    public UnityEvent AlertFired;
    // Use this for initialization
    private void Start () {
        _slider = GetComponent<Slider>();
    }

    public void Add(float valueToAdd) {
        var newValue = _slider.value + valueToAdd;

        if (newValue >= Balance.instance.AlarmLevelFlashight && newValue < Balance.instance.AlarmLevelDoorLight) {
            FlashlightController.instance.enabled = true;
        }

        if (newValue >= Balance.instance.AlarmLevelDoorLight && newValue < Balance.instance.AlarmLevelTopLight) {
            DoorController.instance.enabled = true;
        }
        
        if (newValue >= Balance.instance.AlarmLevelTopLight) {
            //TODO: Turn on the light
        }

        if (AlertFired != null) {
            AlertFired.Invoke();
        }
           
        _slider.value = newValue;
    }
	
	// Update is called once per frame
    private void Update () {
        AlarmTester();

        _slider.value -= (Time.deltaTime * Balance.instance.AlarmDecreasingFactor);
    }

    private void AlarmTester() {
        if (Input.GetKeyDown(key: KeyCode.KeypadPlus)) {
            Add(30);
        }
    }
}
