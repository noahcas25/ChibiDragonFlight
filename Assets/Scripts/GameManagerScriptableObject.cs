using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameManagerScriptableObject", menuName = "ScriptableObjects/Game Manager")]
public class GameManagerScriptableObject : ScriptableObject
{
    public bool _gameState = false;
    public int _score = 0;
    public int _highScore = 0;


    [System.NonSerialized] public UnityEvent<bool> _gameStateEvent;
    [System.NonSerialized] public UnityEvent<int> _scoreChangeEvent;

    private void OnEnable() {
        if(_gameStateEvent == null)
            _gameStateEvent = new UnityEvent<bool>();

        if(_scoreChangeEvent == null)
            _scoreChangeEvent = new UnityEvent<int>();   
        
        if(PlayerPrefs.HasKey("highScore"))
            _highScore = PlayerPrefs.GetInt("highScore");
    }

    private void OnDisable() {
        SetHighScore();
        // GameReset();
        PlayerPrefs.SetInt("highScore", _highScore);
    }

    public void ChangeScore(int score) {
        _score += score;
        _scoreChangeEvent.Invoke(_score);
    }

    public void ChangeGameState(bool status) {
        _gameState = status;
        SetHighScore();
        _gameStateEvent.Invoke(_gameState);
    }

    public void GameReset() {
        ChangeGameState(false);
        ChangeScore(-(_score));
        SceneManager.LoadScene("LastScene");
    }

    private void SetHighScore() {
        if(_score > _highScore)
            _highScore = _score;
    }
}