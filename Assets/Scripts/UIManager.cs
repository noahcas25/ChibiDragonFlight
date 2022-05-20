using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameManagerScriptableObject _gameManager;
    [SerializeField] private GameObject _gameOverCanvas, _startButton, _tapText;
    [SerializeField] private TextMeshProUGUI _scoreText, _highScoreText, _scoreTextGameOver;

    private void Awake() => Application.targetFrameRate = 60;

    public void StartButton() {
        _gameManager.ChangeGameState(true);
        _startButton.SetActive(false);
        _tapText.SetActive(false);
    }

    public void RetryButton() {
        _gameManager.GameReset();
    }

    private void OnEnable() {
        _gameManager._scoreChangeEvent.AddListener(ChangeScoreText);
        _gameManager._gameStateEvent.AddListener(GameOver);
    }

    private void OnDisable() {
        _gameManager._scoreChangeEvent.RemoveListener(ChangeScoreText);
    }

    private void ChangeScoreText(int score) {
        _scoreText.text = "" + score;
    }

    private void GameOver(bool gameState) {
        if(gameState) return;

        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine() {
        yield return new WaitForSeconds(1f);
        _gameOverCanvas.SetActive(true);
        _scoreTextGameOver.text = _scoreText.text;
        _highScoreText.text = _gameManager._highScore + "";
    }
}
