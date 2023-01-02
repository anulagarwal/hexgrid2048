using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevToDev.Analytics;

public class D2D : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID
        DTDAnalytics.Initialize("1b2be080-01d4-0c9a-b937-884b62155598");
#elif UNITY_IOS
        DTDAnalytics.Initialize("IosAppID");
#elif UNITY_WEBGL
        DTDAnalytics.Initialize("WebAppID");
#elif UNITY_STANDALONE_WIN
        DTDAnalytics.Initialize("winAppID");
#elif UNITY_STANDALONE_OSX
        DTDAnalytics.Initialize("OsxAppID");
#elif UNITY_WSA
        DTDAnalytics.Initialize("UwpAppID");
#endif
    }
}
