// using UnityEngine;
// using UnityEngine.Events;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.SceneManagement;

// [CreateAssetMenu(fileName = "GameManagerScriptableObject", menuName = "ScriptableObjects/Game Manager")]
// public class GameManagerScriptableObject : ScriptableObject
// {
//     public bool _gameState = false;
//     public int _score = 0;
//     public int _highScore = 0;
//     public int _skinMaterialNumber = 0;

//     [System.NonSerialized] public UnityEvent<bool> _gameStateEvent;
//     [System.NonSerialized] public UnityEvent<int> _scoreChangeEvent;
//     [System.NonSerialized] public UnityEvent<string> _skinMaterialChangeEvent;

//     private void OnEnable() {
//         if(_gameStateEvent == null)
//             _gameStateEvent = new UnityEvent<bool>();

//         if(_scoreChangeEvent == null) 
//             _scoreChangeEvent = new UnityEvent<int>();

//         if(_skinMaterialChangeEvent == null)
//             _skinMaterialChangeEvent = new UnityEvent<string>();
    

//         // if(PlayerPrefs.HasKey("highScore"))
//         //     _highScore = PlayerPrefs.GetInt("highScore");
//     }

//     private void OnDisable() {
//         SetHighScore();
//         PlayerPrefs.SetInt("highScore", _highScore);
//         ResetVariables();
//         _highScore = 0;
//     }

//     public void ChangeScore(int score) {
//         _score += score;
//         _scoreChangeEvent.Invoke(_score);
//     }

//     public void ChangeGameState(bool status) {
//         _gameState = status;
//         SetHighScore();
//         _gameStateEvent.Invoke(_gameState);
//     }

//     public void ChangeSkinMaterial(int value) {
//         _skinMaterialNumber += value;

//         if(_skinMaterialNumber > 32) 
//             _skinMaterialNumber = 0;
//        if(_skinMaterialNumber < 0) 
//             _skinMaterialNumber = 32;

//         _skinMaterialChangeEvent.Invoke("T_Dragon_" + _skinMaterialNumber);
//         PlayerPrefs.SetInt("skinMaterial", _skinMaterialNumber);
//     }

//     public void ChangeScene(string sceneName) => SceneManager.LoadScene(sceneName); 

//     public void ResetVariables() {
//         ChangeGameState(false);
//         ChangeScore(-(_score));
//     }

//     private void SetHighScore() {
//         if(_score > _highScore) {}
//             _highScore = _score;
//     }
// }