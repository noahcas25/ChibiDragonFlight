using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{

    [SerializeField] private string _iOSGameId = "4840934";
    [SerializeField] private string _iOSAdUnitId = "iOS";
    [SerializeField] private string _androidGameId = "4840935";
    [SerializeField] private string _androidAdUnitId = "Android";
    [SerializeField] bool _testMode = true;
    private string _gameId;
    private string _adUnitId;

    public static AdsManager Instance {get; private set;}

    private void Awake() { 
        Instance = this;
        InitializeAds();
    }

    public void InitializeAds() {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;

        Advertisement.Initialize(_gameId, _testMode, this);
    }
 
    public void OnInitializationComplete() {
        Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message) {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    private void OnEnable() {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    public void PlayAd() {
        LoadAd("Interstitial_");
    }

    public void PlayRewardAd() {
        LoadAd("Rewarded_");
    }

        // Load content to the Ad Unit:
    public void LoadAd(string adType) {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(adType + _adUnitId, this);
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId) {
        Debug.Log("Ad Loaded: " + adUnitId);

        ShowAd(adUnitId);
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowAd(string adUnitId) {
        Advertisement.Show(adUnitId, this);
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) {

            if(showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) {
                Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
                ShopController.Instance.AdReward();
            }
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message){
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
