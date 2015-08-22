using UnityEngine;
using UnityEngine.UI;

public class AlertCounter : MonoSingleton<AlertCounter> {
    private Slider _slider;
    private Balance _balance;
    // Use this for initialization
    private void Start () {
        _slider = GetComponent<Slider>();
        _balance = Balance.instance;
    }

    public void Add(float valueToAdd)
    {
        var newValue = _slider.value + valueToAdd;
        if (newValue >= _slider.maxValue)
        {
            _slider.value = _slider.maxValue;
            //TODO: alarm!!!
            Debug.Log("ALARM!!!");
            return;
        }
        
        _slider.value = newValue;
    }
	
	// Update is called once per frame
    private void Update () {
        AlarmTester();

        _slider.value -= (Time.deltaTime * _balance.AlarmDecreasingFactor);
    }

    private void AlarmTester()
    {
        if (Input.GetKeyDown(key: KeyCode.KeypadPlus))
        {
            Add(30);
        }
    }
}
