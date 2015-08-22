using UnityEngine;
using UnityEngine.UI;

public class AlertCounter : MonoBehaviour {
    private Slider _slider;
    private Balance _balance;
    // Use this for initialization
    void Start () {
        _slider = GetComponent<Slider>();
        _balance = MonoSingleton<Balance>.instance;
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
	void Update () {
        AlarmTester();

        _slider.value -= (Time.deltaTime * _balance.AlarmDecreasingMultiplier);
    }

    private void AlarmTester()
    {
        if (Input.GetKeyDown(key: KeyCode.KeypadPlus))
        {
            Add(30);
        }
    }
}
