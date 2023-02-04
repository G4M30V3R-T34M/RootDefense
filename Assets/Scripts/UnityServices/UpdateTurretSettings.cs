using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class UpdateTurretSettings : MonoBehaviour
{
    [SerializeField]
    private TurretScriptableObject level1Config;
    [SerializeField]
    private TurretScriptableObject level2Config;
    [SerializeField]
    private TurretScriptableObject level3Config;

    public struct userAttributes { };
    public struct appAttributes { };

    Dictionary<int, TurretScriptableObject> confiByLevel = new Dictionary<int, TurretScriptableObject>();

    public void UpdateSettings() {
        confiByLevel.Add(1, level1Config);
        confiByLevel.Add(2, level2Config);
        confiByLevel.Add(3, level3Config);
        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void ApplyRemoteSettings(ConfigResponse configResponse) {
        foreach (KeyValuePair<int, TurretScriptableObject> entry in confiByLevel) {
            // The keys in the remote should be <towername><level>
            var turretConfiguration = RemoteConfigService.Instance.appConfig.GetJson(entry.Value.turretType.ToString() + entry.Key);
            JsonUtility.FromJsonOverwrite(turretConfiguration, entry.Value);
        }

    }
}
