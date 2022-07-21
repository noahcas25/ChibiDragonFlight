using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
   private void Start() {
      if(PlayerPrefs.HasKey("Volume"))
         AudioManager.Instance.ChangeVolume(PlayerPrefs.GetFloat("Volume"));

      StartCoroutine(LoadTimer());
   }

   private IEnumerator LoadTimer() {
      yield return new WaitForSeconds((float) 0.1);
      SceneManager.LoadScene("StartScene");
   }   
}
