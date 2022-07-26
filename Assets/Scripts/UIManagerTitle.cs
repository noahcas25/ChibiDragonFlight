using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTitle : MonoBehaviour
{
     [SerializeField] private GameObject _startMenuCanvas, _optionsCanvas, _volumeSlider;

    public void OpenOptions() { 
        Time.timeScale = 0f;
        _optionsCanvas.SetActive(true);
        _startMenuCanvas.SetActive(false);
    }

    public void CloseOptions() {
        Time.timeScale = 1f;
        _optionsCanvas.SetActive(false);
        _startMenuCanvas.SetActive(true);
        PlayerPrefs.SetFloat("Volume", AudioManager.Instance.GetAudioSource().volume);
    }

    public void VolumeChanged() => AudioManager.Instance.ChangeVolume(_volumeSlider.GetComponent<Slider>().value);

    public void ChangeScene(string sceneName) => GameManager.Instance.ChangeScene(sceneName);
}
