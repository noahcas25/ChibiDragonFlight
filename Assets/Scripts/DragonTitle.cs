using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTitle : MonoBehaviour
{
    private void OnEnable() {
        if(PlayerPrefs.HasKey("skinMaterial"))
            ChangeSkinMaterial("T_Dragon_" + PlayerPrefs.GetInt("skinMaterial"));
    }

    private void ChangeSkinMaterial(string skinMaterial) {
        for(int i = 1; i < 5; i++)
            transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material = Resources.Load(skinMaterial, typeof(Material)) as Material;
    }
}
