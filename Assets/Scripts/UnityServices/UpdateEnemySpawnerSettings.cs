using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class UpdateEnemySpawnerSettings : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnerScriptableObject SpawnerSettings;

    public struct userAttributes { };
    public struct appAttributes { };

    public void UpdateSettings() {
        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }
    
    private void ApplyRemoteSettings(ConfigResponse configResponse) {
        var spawnerConfig = RemoteConfigService.Instance.appConfig.GetJson("SpawnerSettings");
        JsonUtility.FromJsonOverwrite(spawnerConfig, SpawnerSettings);

    }
}
