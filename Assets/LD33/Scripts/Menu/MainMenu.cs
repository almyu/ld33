using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button startButton;
    public Button qualityButton;
    public Button exitButton;
    public Button decreaseButton;
    public Button increaseButton;
    public GameObject preloader;
    public GameObject soundSlider;
    public GameObject musicSlider;

    private int _qualityIndex;
    private Text _qualityButtonText;
    private string[] _qualityNames;

    private void Awake() {
        startButton.onClick.AddListener(OnStartBtnClick);
        exitButton.onClick.AddListener(OnExitBtnClick);
        increaseButton.onClick.AddListener(IncreaseIndex);
        decreaseButton.onClick.AddListener(DecreaseIndex);

        _qualityNames = QualitySettings.names;
        _qualityIndex = _qualityNames.Length-1;
        _qualityButtonText = qualityButton.GetComponentInChildren<Text>();
        SetQuality();
    }

    private void OnStartBtnClick() {
        preloader.SetActive(true);
        StartCoroutine(PreloadScene("Scene"));
    }

    private void OnExitBtnClick() {
        Application.Quit();
    }

    private IEnumerator PreloadScene(string levelName) {
        yield return Application.LoadLevelAsync(levelName);
    }
    
    private void SetQuality() {
        _qualityButtonText.text = _qualityNames[_qualityIndex];
        QualitySettings.SetQualityLevel(_qualityIndex, true);
    }

    private void IncreaseIndex() {
        _qualityIndex += 1;

        if(_qualityIndex >= _qualityNames.Length) {
            _qualityIndex = 0;
        }

        SetQuality();
    }

    private void DecreaseIndex() {
        _qualityIndex -= 1;

        if (_qualityIndex < 0) {
            _qualityIndex = _qualityNames.Length-1;
        }

        SetQuality();
    }
}
