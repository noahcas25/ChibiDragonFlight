using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonShop : MonoBehaviour
{
    public int _skinMaterialNumber = 0;

    private void OnEnable() {
        if(PlayerPrefs.HasKey("skinMaterial"))
            ShopController.Instance.ChangeMaterialButton(PlayerPrefs.GetInt("skinMaterial"));
    }

    public void SaveSkinData() {
        PlayerPrefs.SetInt("skinMaterial", _skinMaterialNumber);
    }

    public void ChangeNumberMaterial(int value) {
        _skinMaterialNumber += value;

        CorrectNumberValue();
        ChangeSkinMaterial("T_Dragon_" + _skinMaterialNumber);
    }

    private void CorrectNumberValue() {
         if(_skinMaterialNumber > 32) 
            _skinMaterialNumber = 0;
        if(_skinMaterialNumber < 0) 
            _skinMaterialNumber = 32;
    }

    public void ChangeSkinMaterial(string skinMaterial) {
        for(int i = 1; i < 5; i++) {
            transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material = Resources.Load(skinMaterial, typeof(Material)) as Material;
        } 
    }
}   
