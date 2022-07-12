using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
     [SerializeField] private AudioSource _audioSource;
     [SerializeField] private AudioClip[] _audioClips;
   

    public static AudioManager Instance {get; private set;}

    private void Awake() => Instance = this;

    public void PlayOneShot(int arrayIndex) => _audioSource.PlayOneShot(_audioClips[arrayIndex]);

    public void ChangeVolume(float value) => _audioSource.volume = value;
}
