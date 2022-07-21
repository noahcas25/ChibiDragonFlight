using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dragonNumText, _currencyText;
    [SerializeField] private DragonShop _dragon;
    [SerializeField] private GameObject playButton;

    private bool[] _unlockedSkins = new bool[16];
    private int _currency;

    public static ShopController Instance {get; private set;}

    public void Awake() {
        Instance = this;
        _unlockedSkins[0] = true;
    }

    private void OnEnable() {
        LoadData();
        LoadPrefs();
    }

    private void OnDisable() {
        SaveData();
        SavePrefs();
    }

    public void ChangeMaterialButton(int value) {
        _dragon.ChangeNumberMaterial(value);
        _dragonNumText.text = _dragon._skinMaterialNumber + 1 + "";

        if(_unlockedSkins[_dragon._skinMaterialNumber]) 
            playButton.SetActive(true);
        else 
            playButton.SetActive(false);
    }

    public void UnlockSkin(int value) {
        if(_currency >= value) {
            _currency -= value;
            _unlockedSkins[_dragon._skinMaterialNumber] = true;
            _currencyText.text = _currency + " ";
            ChangeMaterialButton(0);
        } else{
            _currencyText.GetComponent<Animator>().Play("CurrencyJiggle", 0, 0.25f);
            AudioManager.Instance.PlayOneShot(3);
        }
    }

    public void ChangeScene(string sceneName) {
        if(sceneName == "GameScene") {
            _dragon.SaveSkinData();
        }
        SceneManager.LoadScene(sceneName);
    } 

    private void SaveData() {
        using(FileStream file = File.Open(Application.persistentDataPath + "GameSaveTest1.dat", FileMode.Create, FileAccess.Write, FileShare.None)) {
            using(var writer = new BinaryWriter(file, Encoding.UTF8, false)) {
                for(int i = 0; i < _unlockedSkins.Length; i++)
                    writer.Write(_unlockedSkins[i]);
            }
        }
    }

    private void LoadData() {
        if(!File.Exists(Application.persistentDataPath + "GameSaveTest1.dat")) return;

        using(FileStream file = File.Open(Application.persistentDataPath + "GameSaveTest1.dat", FileMode.Open, FileAccess.Read, FileShare.None)) {
            using(var reader = new BinaryReader(file, Encoding.UTF8, false)) {
                for(int i = 0; i < _unlockedSkins.Length; i++) {
                    if(reader.BaseStream.Position == reader.BaseStream.Length){
                        print("EndOfFileReached-IssueWithUnlockedSkins");
                        return;
                    }

                    _unlockedSkins[i] = reader.ReadBoolean();
                }
            }
        }
    }

    private void LoadPrefs() {
        if(PlayerPrefs.HasKey("Currency"))
            _currency = PlayerPrefs.GetInt("Currency");

        ChangeMaterialButton(0);
        _currencyText.text = _currency + " ";
    }

    private void SavePrefs() {
        PlayerPrefs.SetInt("Currency", _currency);
    }

    // Function to initiate an ads to play
    public void PlayAd()  {
         AdsManager.Instance.PlayRewardAd();
    }

    // // Reward for watching ad
    public void AdReward() {
         _currency += 100; 
         _currencyText.text = _currency + "";
    }
}