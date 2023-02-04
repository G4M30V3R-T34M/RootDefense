using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;
using FeTo.SOArchitecture;

public class UnityLogIn : MonoBehaviour
{
    public GameEvent UnityLoggedIn;
    private async void Awake() {
        await InitializeRemoteConfigAsync();
        UnityLoggedIn.Raise();
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
}
