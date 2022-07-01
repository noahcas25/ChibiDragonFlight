using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private DragonShop _dragon;

    public static ShopController Instance {get; private set;}

    public void Awake() => Instance = this;

    public void ChangeMaterialButton(int value) {
        _dragon.ChangeNumberMaterial(value);
        _scoreText.text = _dragon._skinMaterialNumber + 1 + "";
    }

    public void ChangeScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
