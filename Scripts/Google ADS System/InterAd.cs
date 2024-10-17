using UnityEngine;
using GoogleMobileAds.Api;

public class InterAd : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    [SerializeField] private bool isADKeyTest;

    private string interstitialUnitId = "ca-app-pub-3940256099942544/1033173712";

    public static InterAd Instance;

    private void Awake()
    {
        if (!isADKeyTest) interstitialUnitId = "ca-app-pub-4275611682046066/5498610436";
    }

    private void Start() => Instance = this;

    private void OnEnable()
    {
        interstitialAd = new InterstitialAd(interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd()
    {
        if (interstitialAd.IsLoaded()) interstitialAd.Show();
    }
}
