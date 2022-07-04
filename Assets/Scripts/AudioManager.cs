using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
     [SerializeField] public AudioSource _audioSource;
     [SerializeField] private AudioClip[] _audioClips;
   

    public static AudioManager Instance {get; private set;}

    private void Awake() => Instance = this;

    public void PlayOneShot(int arrayIndex) {
        print(arrayIndex);
        if(_audioSource==null)
           _audioSource = this.GetComponent<AudioSource>();
        if(_audioClips[0]==null)
            print("AUDIOclip");    
        else         
            _audioSource.PlayOneShot(_audioClips[arrayIndex]);
    }


}
