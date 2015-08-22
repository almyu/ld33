using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoSingleton<GameOverController> {
    public GameObject gameOverText;
    public GameObject restartButton;

    public void ShowGameOver(bool win) {
        gameOverText.SetActive(true);
        restartButton.GetComponent<Button>().onClick.AddListener(Restart);

        var text = string.Empty;
        text += win ? "You win!" : "You've lost!";
        text += "\n";
        text += "Your score: " + ScoreCounter.instance.GetScore();

        gameOverText.GetComponent<Text>().text = text;
    }

    private void Restart() {
        Application.LoadLevel(Application.loadedLevel);
    }
}
