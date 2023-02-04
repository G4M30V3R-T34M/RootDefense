using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class UpdateEnemySettings : MonoBehaviour
{
    [SerializeField]
    List<EnemyScriptableObject> enemySettings = new List<EnemyScriptableObject>();

    public struct userAttributes { };
    public struct appAttributes { };

    public void UpdateSettings() {
        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void ApplyRemoteSettings(ConfigResponse configResponse) {
        foreach(EnemyScriptableObject setting in enemySettings) {
            var EnemyConfiguration = RemoteConfigService.Instance.appConfig.GetJson(setting.enemyType.ToString());
            JsonUtility.FromJsonOverwrite(EnemyConfiguration, setting);
        }
    }
}
