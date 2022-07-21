using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool _gameState = false;
    private int _score = 0;
    private int _highScore = 0;
    private int _currency = 0;

    [System.NonSerialized] public UnityEvent<bool> _gameStateEvent;
    [System.NonSerialized] public UnityEvent<int> _scoreChangeEvent;

    public static GameManager Instance {get; private set;}

    private void Awake() => Instance = this;

    private void OnEnable() {
        if(_gameStateEvent == null)
            _gameStateEvent = new UnityEvent<bool>();

        if(_scoreChangeEvent == null) 
            _scoreChangeEvent = new UnityEvent<int>();

        if(PlayerPrefs.HasKey("highScore"))
            _highScore = PlayerPrefs.GetInt("highScore");

        if(PlayerPrefs.HasKey("Currency"))
            _currency = PlayerPrefs.GetInt("Currency");
    }

    private void OnDisable() {
        SavePrefs();
        ResetVariables();
    }

    public void ChangeScore(int score) {
        if(score >= 1)
           AudioManager.Instance.PlayOneShot(1);

        _score += score;
        _scoreChangeEvent.Invoke(_score);
    }

    public void ChangeGameState(bool status) {
        _gameState = status;
        SetHighScore();
        _gameStateEvent.Invoke(_gameState);
    }

    public void ResetVariables() {
        ChangeGameState(false);
        ChangeScore(-(_score));
    }

    private void SetHighScore() {
        if(_score > _highScore)
            _highScore = _score;

        if(_score > 0)
            _currency += _score;
    }

    public int GetScore() => _score;

    public int GetHighScore() => _highScore;

    public int GetCurrency() => _currency;

    public bool GetGameState() => _gameState;

    private void SavePrefs() {
        PlayerPrefs.SetInt("highScore", _highScore);
        PlayerPrefs.SetInt("Currency",  _currency);
    }
    
    public void ChangeScene(string sceneName) => SceneManager.LoadScene(sceneName); 
}
