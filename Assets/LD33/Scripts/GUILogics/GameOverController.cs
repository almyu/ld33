using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoSingleton<GameOverController> {
    private GameObject _gameOverText;
    private GameObject _restartButton;
    private bool _gameOver = false;
    private void Awake() {
        _gameOverText = GameObject.FindGameObjectWithTag("GameOverText");
        _restartButton = GameObject.FindGameObjectWithTag("RestartButton");
    }

    public void ShowGameOver(bool win) {
        if (_gameOver)
            return;
        _gameOver = true;

        _restartButton.GetComponent<Image>().enabled = true;
        _restartButton.GetComponent<Button>().enabled = true;
        
        _restartButton.GetComponent<Button>().onClick.AddListener(Restart);

        var text = string.Empty;
        text += win ? "You win!" : "You've lost!";
        text += "\n";
        text += "\n";
        text += TimeCounter.instance.GetElapsedTime();
        text += "\n";
        text += "\n";
        text += "Score: " + ScoreCounter.instance.GetFinalScore();

        _gameOverText.GetComponent<Text>().text = text;
    }

    private void Restart() {
        Application.LoadLevel(Application.loadedLevel);
    }
}
