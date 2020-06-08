using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellPlusSDK;

using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class AdManager : MonoSingleton<AdManager>
{
    private const string TAPSELL_KEY = "bdstiialordtqparkaoceqlcjflpbdgdobkhhrdtsdiqqgdeqmjqiqicqjbsfrsibjjhhq";
    private const String CHOSEN_Ad_FastBanner = "5ed6146129cf0900017963d2";
    private static String CHOSEN_Ad_RewardVideo = "5ed91d9186b1d800016dc693";

    private String[] Ad_RewardVideo =
    {
        "5ed91d9186b1d800016dc693",
        "5cfaa802e8d17f0001ffb28e",
        "5cfaa8aee8d17f0001ffb28f",
        "5cfaa8eae8d17f0001ffb291",
        "5cfaa8cee8d17f0001ffb290",
        "5cfaa838aede570001d55538",
        "5d3362766de9f600013662d5",
        "5d3eb48c3aef7a0001406f84",
        "5d3eb55a7a9b060001892442"
    };
    
    /*
    private Action<string> onNoAdAvailableAction;
    private Action<TapsellError> onErrorAction;
    private Action<string> onNoNetworkAction;
    private Action<TapsellPlus> onExpiringAction;
    
    public static Action<TapsellPlusFinishedResult> onRewardAction; //classes that need to do in action time should subscribe
    public static Action<TapsellPlus> onAdAvailableAction; //classes that need to do in action time should subscribe
    public static Action onAnyErrorAdAction;
    
    private TapsellShowOptions defaultOption;
    */
//    public TapsellPlus tapsellFastBanner = null;
//    public TapsellPlus tapselRewardVideo = null;
    public bool tapsellFastBannerAvailable = false;
    public bool tapselRewardVideoAvailable = false;

    private void Start()
    {
        tapselRewardVideoAvailable = false;
        tapsellFastBannerAvailable = false;
        /*
        for (int i = 0; i < onAdAvailableAction.GetInvocationList().Length; i++)
        {
            Debug.Log(onAdAvailableAction.GetInvocationList()[i].Method);
        }*/
    }

    protected override void initialization()
    {
        /*
        defaultOption = new TapsellShowOptions();
        defaultOption.backDisabled = false;
        defaultOption.immersiveMode = false;
        defaultOption.rotationMode = TapsellShowOptions.ROTATION_UNLOCKED;
        defaultOption.showDialog = true;
        */
        TapsellPlus.initialize(TAPSELL_KEY);
        /*
        onAdAvailableAction += OnAdAvailable;
        onErrorAction += OnAdError;
        onExpiringAction += OnExpiring;
        onNoNetworkAction += OnNoNetwork;
        onNoAdAvailableAction += OnNoAdAvailable;
        */
      //  onAnyErrorAdAction += OnAnyErrorAd;
        removeIdAndRequestNewOne(null);
    }
    
    /*
    private void OnAdAvailable(TapsellPlus TapsellPlus)
    {
        if (TapsellPlus.zoneId.Equals(TAPSEL_Ad_FastBanner))
        {
            tapsellFastBanner = TapsellPlus;
            tapsellFastBannerAvailable = true;
        }
        else if(TapsellPlus.zoneId.Equals(TAPSEL_Ad_RewardVideo))
        {
            tapselRewardVideo = TapsellPlus;
            tapselRewardVideoAvailable = true;
        }
    }
    */

    private void OnResponse(String zoneId)
    {
        if (zoneId.Equals(CHOSEN_Ad_FastBanner))
        {
          //  tapsellFastBanner = TapsellPlus;
            tapsellFastBannerAvailable = true;
        }
        else if(zoneId.Equals(CHOSEN_Ad_RewardVideo))
        {
           // tapselRewardVideo = TapsellPlus;
            tapselRewardVideoAvailable = true;
        }
    }
    private void OnAnyErrorAd()
    {
      //  tapsellFastBanner = null;
    }

    private void removeIdAndRequestNewOne(String zoneId)
    {
        if (zoneId == null)
        {
           // tapsellFastBanner = null;
           // tapselRewardVideo = null;
            tapselRewardVideoAvailable = false;
            tapsellFastBannerAvailable = false;
            Invoke("requestNewFastBannerAd",0.1f);
            Invoke("requestNewRewardVideo",0.3f);
            return;

        }
        if (CHOSEN_Ad_FastBanner.Equals(zoneId))
        {
            //tapsellFastBanner = null;
            tapsellFastBannerAvailable = false;
            Invoke("requestNewFastBannerAd",0.1f);

        }
        else if(CHOSEN_Ad_RewardVideo.Equals(zoneId))
        {
            tapselRewardVideoAvailable = false;
           // tapselRewardVideo = null;
           Invoke("requestNewFastBannerAd",0.15f);
        }
        else
        {
            tapselRewardVideoAvailable = false;
            tapsellFastBannerAvailable = false;
            Invoke("requestNewFastBannerAd",0.1f);
            Invoke("requestNewFastBannerAd",0.15f);  
        }
    }


    /*
    public void playAd(TapsellPlus TapsellPlus,Action<TapsellPlusFinishedResult> onComplete)
    {
        
        Tapsell.ShowAd(TapsellPlus, defaultOption);
        Tapsell.SetRewardListener((TapsellPlusFinishedResult result) =>
        {
            onComplete(result);
            removeIdAndRequestNewOne(result.zoneId);
        });
        
    }*/

    public void playRewardVideoAd(Action<bool>onComplete)
    {

        playAd(CHOSEN_Ad_RewardVideo,onComplete);
    }
    
    public void playFastBanner(Action<bool>onComplete)
    {
        playAd(CHOSEN_Ad_FastBanner,onComplete);
    }
    private void playAd(String ZONE_ID,Action<bool>onComplete)
    {
        TapsellPlus.showAd (ZONE_ID,
            (string zoneId) => {
                Debug.Log ("onOpenAd " + zoneId);
            },
            (string zoneId) => {
                Debug.Log ("onCloseAd " + zoneId);
                onComplete(false);
                removeIdAndRequestNewOne(ZONE_ID);
            },
            (string zoneId) => {
                Debug.Log ("onReward " + zoneId);
                onComplete(true);
            },
            (TapsellError error) => {
                Debug.Log ("onError " + error.message);
                onComplete(false);

            }
        );
        
    }
    
    
    private void OnAdError(TapsellError tapsellError)
    {
        removeIdAndRequestNewOne(tapsellError.zoneId);
       // onAnyErrorAdAction();
    }
    /*
    private void OnNoAdAvailable(string noAdd)
    {
        removeIdAndRequestNewOne(null);
        onAnyErrorAdAction();

    }
        
    private void OnExpiring(TapsellPlus TapsellPlus)
    {
        
        removeIdAndRequestNewOne(TapsellPlus.zoneId);
        onAnyErrorAdAction();
    }
    
    private void OnNoNetwork(string networkError)
    {
        
        removeIdAndRequestNewOne(null);
        onAnyErrorAdAction();

    }
    
    */
    
    private void requestNewFastBannerAd()
    {
        requestNewAd(CHOSEN_Ad_FastBanner,true);
    }
    
    private void requestNewRewardVideo()
    {
       // int r = Random.Range(0, Ad_RewardVideo.Length);
       // CHOSEN_Ad_RewardVideo = Ad_RewardVideo[r];
        requestNewAd(CHOSEN_Ad_RewardVideo,true);
    }

    private void requestNewAd(string zoneId,bool isCashed)
    {
        /*
        Tapsell.RequestAd(zoneId, isCashed,onAdAvailableAction,
            onNoAdAvailableAction,onErrorAction,
            onNoNetworkAction,onExpiringAction);
            */
        TapsellPlus.requestRewardedVideo(zoneId, OnResponse, OnAdError);
    }
    
    
}
