using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {

    private GameObject _mainMenuButton;
    private GameObject _continueButton;
    private GameObject _pauseButton;

    private void Awake() {
        _mainMenuButton = GameObject.FindGameObjectWithTag("MainMenuBtn");
        _continueButton = GameObject.FindGameObjectWithTag("ContinueBtn");
        _pauseButton = GameObject.FindGameObjectWithTag("PauseText"); 

        _mainMenuButton.GetComponent<Button>().onClick.AddListener(OnMainMenuBtn);
        _continueButton.GetComponent<Button>().onClick.AddListener(OnContinueBtn);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ShowPause(true);
        }
    }

    public void ShowPause(bool show) {
        if (show) {
            PauseGameController.instance.enabled = true;
        }

        _pauseButton.GetComponent<Text>().enabled = show;
        
        _mainMenuButton.GetComponent<Button>().enabled = show;
        _mainMenuButton.GetComponentInChildren<Text>().enabled = show;
        
        _continueButton.GetComponent<Button>().enabled = show;
        _continueButton.GetComponentInChildren<Text>().enabled = show;
    }

    private void OnMainMenuBtn() {
        Application.LoadLevel("MainMenu");
    }

    private void OnContinueBtn() {
        ShowPause(false);
        PauseGameController.instance.enabled = false;
    }
}
