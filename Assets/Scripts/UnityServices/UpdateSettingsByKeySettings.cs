using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class UpdateSettingsByKeySettings : MonoBehaviour
{
    [SerializeField]
    string remoteKey;
    [SerializeField]
    private ScriptableObject scriptableSettings;

    public struct userAttributes { };
    public struct appAttributes { };

    public void UpdateSettings() {
        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }
    
    private void ApplyRemoteSettings(ConfigResponse configResponse) {
        var remoteConfig = RemoteConfigService.Instance.appConfig.GetJson(remoteKey);
        JsonUtility.FromJsonOverwrite(remoteConfig, scriptableSettings);

    }
}
