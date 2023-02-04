using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.Core;
using Unity.Services.Authentication;

using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using UnityEditor.PackageManager;
using System.Xml;

public class BaseTurret : MonoBehaviour
{
    [SerializeField] 
    private TurretScriptableObject level1Config;
    [SerializeField] 
    private TurretScriptableObject level2Config;
    [SerializeField] 
    private TurretScriptableObject level3Config;

    private int currentLevel = 1;
    private int maxLevel = 3;
    private TurretScriptableObject currentSettings;

    Dictionary<int, TurretScriptableObject> confiByLevel = new Dictionary<int, TurretScriptableObject>();

    public struct userAttributes { };
    public struct appAttributes { };

    private async void Awake() {
        // Init level dictionary
        InitLevelDict();

        // Get data from remote
        await InitializeRemoteConfigAsync();
        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        currentSettings = confiByLevel[currentLevel];
    }

    private void ApplyRemoteSettings(ConfigResponse configResponse) {
        foreach (KeyValuePair<int, TurretScriptableObject> entry in confiByLevel) {
            // The keys in the remote should be <towername><level>
            var turretConfiguration = RemoteConfigService.Instance.appConfig.GetJson(entry.Value.turretType.ToString() + entry.Key);
            JsonUtility.FromJsonOverwrite(turretConfiguration, entry.Value);
        }
        
    }

    private void InitLevelDict() {
        confiByLevel.Add(1, level1Config);
        confiByLevel.Add(2, level2Config);
        confiByLevel.Add(3, level3Config);
    }

    async Task InitializeRemoteConfigAsync() {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // options can be passed in the initializer, e.g if you want to set analytics-user-id or an environment-name use the lines from below:
        // var options = new InitializationOptions()
        //   .SetOption("com.unity.services.core.analytics-user-id", "my-user-id-1234")
        //   .SetOption("com.unity.services.core.environment-name", "production");
        // await UnityServices.InitializeAsync(options);

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn) {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    // This will be a suscribed to an event
    public void Upgrade() {
        if (CanBeUpgraded()) {
            currentLevel += 1;
            currentSettings = confiByLevel[currentLevel];
        }
    }

    public bool CanBeUpgraded() {
        return currentLevel < maxLevel;
    }
}
