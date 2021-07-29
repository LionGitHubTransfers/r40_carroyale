using UnityEngine;
using com.adjust.sdk;
using GameAnalyticsSDK;

public class IntegrationManager : MonoBehaviour
{
    public static IntegrationManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
#if UNITY_IOS
        /* Mandatory - set your iOS app token here */
        InitAdjust("000000000000");
#elif UNITY_ANDROID
        /* Mandatory - set your Android app token here */
        InitAdjust("8n3u6rkxar28");
#endif

        GameAnalytics.Initialize();
    }

    public void OnLevelStart(int level)
    {
        #region GameAnalytics
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level_" + level);
        #endregion
    }

    public void OnLevelFinish(int level)
    {

        #region GameAnalytics
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level_" + level);
        #endregion
    }

    public void OnLevelFail(int level)
    {

        #region GameAnalytics
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level_" + level);
        #endregion
    }

    private void InitAdjust(string adjustAppToken)
    {
        var adjustConfig = new AdjustConfig(
            adjustAppToken,
            AdjustEnvironment.Production, // AdjustEnvironment.Sandbox to test in dashboard
            true
        );
        adjustConfig.setLogLevel(AdjustLogLevel.Info); // AdjustLogLevel.Suppress to disable logs
        adjustConfig.setSendInBackground(true);
        new GameObject("Adjust").AddComponent<Adjust>(); // do not remove or rename

        // Adjust.addSessionCallbackParameter("foo", "bar"); // if requested to set session-level parameters

        //adjustConfig.setAttributionChangedDelegate((adjustAttribution) => {
        //  Debug.LogFormat("Adjust Attribution Callback: ", adjustAttribution.trackerName);
        //});

        Adjust.start(adjustConfig);
    }
}
