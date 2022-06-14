using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameManagerScriptableObject _gameManager;
    [SerializeField] private GameObject _gameOverCanvas, _optionsCanvas, _startMenuCanvas, _startButton, _pauseButton, _tapText;
    [SerializeField] private TextMeshProUGUI _scoreText, _highScoreText, _scoreTextGameOver;
    [SerializeField] private GameObject _gameOverFlash;

    private void Awake() => Application.targetFrameRate = 60;

    public void StartButton() {
        _gameManager.ChangeGameState(true);
        _startButton.SetActive(false);
        _pauseButton.SetActive(true);
        _tapText.SetActive(false);
    }

    public void OpenOptions() { 
        Time.timeScale = 0f;
        _optionsCanvas.SetActive(true);

        if(_startMenuCanvas!=null)
            _startMenuCanvas.SetActive(false);
    }

    public void CloseOptions() {
        Time.timeScale = 1f;
        _optionsCanvas.SetActive(false);

        if(_startMenuCanvas!=null)
            _startMenuCanvas.SetActive(true);
    }
    public void ChangeScene(string sceneName) {
        _gameManager.ChangeScene(sceneName);
    }

    private void OnEnable() {
        _gameManager._scoreChangeEvent.AddListener(ChangeScoreText);
        _gameManager._gameStateEvent.AddListener(GameOver);
    }

    private void OnDisable() {
        _gameManager._scoreChangeEvent.RemoveListener(ChangeScoreText);
        _gameManager.ResetVariables();
        Time.timeScale = 1f;
    }

    private void ChangeScoreText(int score) {
        _scoreText.text = "" + score;
    }

    private void GameOver(bool gameState) {
        if(gameState) return;

        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine() {
        _gameOverFlash.SetActive(true);
        yield return new WaitForSeconds(1f);
        _gameOverCanvas.SetActive(true);
        _scoreTextGameOver.text = _scoreText.text;
        _highScoreText.text = _gameManager._highScore + "";
    }
}
